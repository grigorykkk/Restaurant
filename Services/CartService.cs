using Microsoft.EntityFrameworkCore;

namespace Restaurant_site.Services;

public class CartService
{
    private readonly ApplicationDbContext _context;
    private string _sessionId = Guid.NewGuid().ToString();
    private List<CartItem> _cachedItems = new();

    public const decimal MinOrderAmount = 500; // Минимальная сумма заказа

    public event Action? CartChanged;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Синхронные свойства для использования в Razor компонентах
    public List<CartItem> Items
    {
        get
        {
            _cachedItems = GetItemsAsync().GetAwaiter().GetResult();
            return _cachedItems;
        }
    }

    public int TotalItems => GetTotalItemsAsync().GetAwaiter().GetResult();

    public decimal TotalPrice => GetTotalPriceAsync().GetAwaiter().GetResult();

    public bool IsMinOrderAmountMet => IsMinOrderAmountMetAsync().GetAwaiter().GetResult();

    public void SetSessionId(string sessionId)
    {
        _sessionId = sessionId;
    }

    private async Task<Cart> GetOrCreateCartAsync()
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Dish)
            .ThenInclude(d => d.DishType)
            .FirstOrDefaultAsync(c => c.SessionId == _sessionId);

        if (cart == null)
        {
            cart = new Cart
            {
                SessionId = _sessionId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<List<CartItem>> GetItemsAsync()
    {
        var cart = await GetOrCreateCartAsync();
        return cart.CartItems.ToList();
    }

    public async Task<int> GetTotalItemsAsync()
    {
        var items = await GetItemsAsync();
        return items.Sum(item => item.Quantity);
    }

    public async Task<decimal> GetTotalPriceAsync()
    {
        var items = await GetItemsAsync();
        return items.Sum(item => item.TotalPrice);
    }

    public async Task<bool> IsMinOrderAmountMetAsync()
    {
        var totalPrice = await GetTotalPriceAsync();
        return totalPrice >= MinOrderAmount;
    }

    public async Task AddToCartAsync(Dish dish, int quantity = 1)
    {
        var cart = await GetOrCreateCartAsync();
        var existingItem = cart.CartItems.FirstOrDefault(item => item.IdDish == dish.IdDish);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new CartItem
            {
                IdCart = cart.IdCart,
                IdDish = dish.IdDish,
                Dish = dish,
                Quantity = quantity,
                AddedAt = DateTime.Now
            };
            _context.CartItems.Add(newItem);
        }

        cart.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        CartChanged?.Invoke();
    }

    public async Task RemoveFromCartAsync(int dishId)
    {
        var cart = await GetOrCreateCartAsync();
        var item = cart.CartItems.FirstOrDefault(item => item.IdDish == dishId);

        if (item != null)
        {
            _context.CartItems.Remove(item);
            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            CartChanged?.Invoke();
        }
    }

    public async Task UpdateQuantityAsync(int dishId, int newQuantity)
    {
        if (newQuantity <= 0)
        {
            await RemoveFromCartAsync(dishId);
            return;
        }

        var cart = await GetOrCreateCartAsync();
        var item = cart.CartItems.FirstOrDefault(item => item.IdDish == dishId);

        if (item != null)
        {
            item.Quantity = newQuantity;
            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            CartChanged?.Invoke();
        }
    }

    public async Task ClearCartAsync()
    {
        var cart = await GetOrCreateCartAsync();

        _context.CartItems.RemoveRange(cart.CartItems);
        cart.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        CartChanged?.Invoke();
    }

    public async Task<int> GetQuantityAsync(int dishId)
    {
        var cart = await GetOrCreateCartAsync();
        var item = cart.CartItems.FirstOrDefault(item => item.IdDish == dishId);
        return item?.Quantity ?? 0;
    }

    public async Task<bool> IsInCartAsync(int dishId)
    {
        var cart = await GetOrCreateCartAsync();
        return cart.CartItems.Any(item => item.IdDish == dishId);
    }

    // Синхронные обёртки для использования в Razor компонентах
    public void AddToCart(Dish dish, int quantity = 1)
    {
        AddToCartAsync(dish, quantity).GetAwaiter().GetResult();
    }

    public void RemoveFromCart(int dishId)
    {
        RemoveFromCartAsync(dishId).GetAwaiter().GetResult();
    }

    public void UpdateQuantity(int dishId, int newQuantity)
    {
        UpdateQuantityAsync(dishId, newQuantity).GetAwaiter().GetResult();
    }

    public void ClearCart()
    {
        ClearCartAsync().GetAwaiter().GetResult();
    }

    public int GetQuantity(int dishId)
    {
        return GetQuantityAsync(dishId).GetAwaiter().GetResult();
    }

    public bool IsInCart(int dishId)
    {
        return IsInCartAsync(dishId).GetAwaiter().GetResult();
    }
}

