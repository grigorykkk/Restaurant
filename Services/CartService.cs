using Restaurant_site.Models;

namespace Restaurant_site.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();

    public const decimal MinOrderAmount = 500; // Минимальная сумма заказа

    public event Action? CartChanged;

    public List<CartItem> Items => _items;

    public int TotalItems => _items.Sum(item => item.Quantity);

    public decimal TotalPrice => _items.Sum(item => item.TotalPrice);

    public bool IsMinOrderAmountMet => TotalPrice >= MinOrderAmount;

    public void AddToCart(MenuItem menuItem, int quantity = 1)
    {
        var existingItem = _items.FirstOrDefault(item => item.MenuItem.Id == menuItem.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                MenuItem = menuItem,
                Quantity = quantity
            });
        }

        CartChanged?.Invoke();
    }

    public void RemoveFromCart(int menuItemId)
    {
        var item = _items.FirstOrDefault(item => item.MenuItem.Id == menuItemId);
        if (item != null)
        {
            _items.Remove(item);
            CartChanged?.Invoke();
        }
    }

    public void UpdateQuantity(int menuItemId, int newQuantity)
    {
        var item = _items.FirstOrDefault(item => item.MenuItem.Id == menuItemId);
        if (item != null)
        {
            if (newQuantity <= 0)
            {
                RemoveFromCart(menuItemId);
            }
            else
            {
                item.Quantity = newQuantity;
                CartChanged?.Invoke();
            }
        }
    }

    public void ClearCart()
    {
        _items.Clear();
        CartChanged?.Invoke();
    }

    public int GetQuantity(int menuItemId)
    {
        var item = _items.FirstOrDefault(item => item.MenuItem.Id == menuItemId);
        return item?.Quantity ?? 0;
    }

    public bool IsInCart(int menuItemId)
    {
        return _items.Any(item => item.MenuItem.Id == menuItemId);
    }
}
