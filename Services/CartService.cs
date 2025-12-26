using Microsoft.EntityFrameworkCore;
using Restaurant_site.Data;

namespace Restaurant_site.Services;

public class CartService
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;
    private string _sessionId = Guid.NewGuid().ToString();

    public const decimal MinOrderAmount = 500; // Минимальная сумма заказа

    public event Action? CartChanged;

    public CartService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public void SetSessionId(string sessionId)
    {
        if (_sessionId != sessionId)
        {
            _sessionId = sessionId;
            // Notify components that the session has changed, forcing a reload of data
            CartChanged?.Invoke();
        }
    }

    public string GetSessionId()
    {
        return _sessionId;
    }

    private async Task<Cart> GetOrCreateCartAsync(ApplicationDbContext context)
    {
        var cart = await context.Carts
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
            context.Carts.Add(cart);
            await context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<List<CartItem>> GetItemsAsync()
    {
        using var context = await _factory.CreateDbContextAsync();
        var cart = await GetOrCreateCartAsync(context);
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
        using var context = await _factory.CreateDbContextAsync();
        var cart = await GetOrCreateCartAsync(context);
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
                Dish = null, // To avoid tracking issues with detached entity
                Quantity = quantity,
                AddedAt = DateTime.Now
            };
            // Ensure we don't try to add a duplicate tracked entity if Dish is tracked elsewhere
            // Better to just set IDs for new item
            context.CartItems.Add(newItem);
        }

        cart.UpdatedAt = DateTime.Now;
        await context.SaveChangesAsync();
        CartChanged?.Invoke();
    }

    public async Task RemoveFromCartAsync(int dishId)
    {
        using var context = await _factory.CreateDbContextAsync();
        var cart = await GetOrCreateCartAsync(context);
        var item = cart.CartItems.FirstOrDefault(item => item.IdDish == dishId);

        if (item != null)
        {
            context.CartItems.Remove(item);
            cart.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
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

        using var context = await _factory.CreateDbContextAsync();
        var cart = await GetOrCreateCartAsync(context);
        var item = cart.CartItems.FirstOrDefault(item => item.IdDish == dishId);

        if (item != null)
        {
            item.Quantity = newQuantity;
            cart.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
            CartChanged?.Invoke();
        }
    }

    public async Task ClearCartAsync()
    {
        using var context = await _factory.CreateDbContextAsync();
        var cart = await GetOrCreateCartAsync(context);

        context.CartItems.RemoveRange(cart.CartItems);
        cart.UpdatedAt = DateTime.Now;
        await context.SaveChangesAsync();
        CartChanged?.Invoke();
    }

    public async Task<int> GetQuantityAsync(int dishId)
    {
        using var context = await _factory.CreateDbContextAsync();
        var cart = await GetOrCreateCartAsync(context);
        var item = cart.CartItems.FirstOrDefault(item => item.IdDish == dishId);
        return item?.Quantity ?? 0;
    }

    public async Task<bool> IsInCartAsync(int dishId)
    {
        using var context = await _factory.CreateDbContextAsync();
        var cart = await GetOrCreateCartAsync(context);
        return cart.CartItems.Any(item => item.IdDish == dishId);
    }
}
