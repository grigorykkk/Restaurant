using Microsoft.EntityFrameworkCore;

namespace Restaurant_site.Services;

public class MenuService
{
    private readonly ApplicationDbContext _context;

    public MenuService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DishType>> GetAllDishTypesAsync()
    {
        return await _context.DishTypes.ToListAsync();
    }

    public async Task<List<Dish>> GetAllDishesAsync()
    {
        return await _context.Dishes
            .Include(d => d.DishType)
            .ToListAsync();
    }

    public async Task<List<Dish>> GetDishesByCategoryAsync(int categoryId)
    {
        if (categoryId == 0)
        {
            return await GetAllDishesAsync();
        }

        return await _context.Dishes
            .Include(d => d.DishType)
            .Where(d => d.IdDishType == categoryId)
            .ToListAsync();
    }

    public async Task<Dish?> GetDishByIdAsync(int id)
    {
        return await _context.Dishes
            .Include(d => d.DishType)
            .FirstOrDefaultAsync(d => d.IdDish == id);
    }
}
