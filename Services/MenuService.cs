using Microsoft.EntityFrameworkCore;
using Restaurant_site.Data;

namespace Restaurant_site.Services;

public class MenuService
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public MenuService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<DishType>> GetAllDishTypesAsync()
    {
        using var context = await _factory.CreateDbContextAsync();
        return await context.DishTypes.ToListAsync();
    }

    public async Task<List<Dish>> GetAllDishesAsync()
    {
        using var context = await _factory.CreateDbContextAsync();
        return await context.Dishes
            .Include(d => d.DishType)
            .ToListAsync();
    }

    public async Task<List<Dish>> GetDishesByCategoryAsync(int categoryId)
    {
        using var context = await _factory.CreateDbContextAsync();
        if (categoryId == 0)
        {
            return await context.Dishes
                .Include(d => d.DishType)
                .ToListAsync();
        }

        return await context.Dishes
            .Include(d => d.DishType)
            .Where(d => d.IdDishType == categoryId)
            .ToListAsync();
    }

    public async Task<Dish?> GetDishByIdAsync(int id)
    {
        using var context = await _factory.CreateDbContextAsync();
        return await context.Dishes
            .Include(d => d.DishType)
            .FirstOrDefaultAsync(d => d.IdDish == id);
    }
}
