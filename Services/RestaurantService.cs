using Restaurant_site.Data;
using Microsoft.EntityFrameworkCore;

namespace Restaurant_site.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public RestaurantService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Dishes
                .Include(d => d.DishType)
                .ToListAsync();
        }

        public async Task<List<Table>> GetTablesAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Tables
                .Include(t => t.TableType)
                .ToListAsync();
        }

        public async Task<List<TableType>> GetTableTypesAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.TableTypes.ToListAsync();
        }

        public async Task<List<DishType>> GetDishTypesAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.DishTypes.ToListAsync();
        }

        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            try
            {
                using var context = await _factory.CreateDbContextAsync();
                context.Reservations.Add(reservation);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            try
            {
                using var context = await _factory.CreateDbContextAsync();
                context.Orders.Add(order);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<User?> GetUserByPhoneAsync(string phoneNum)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Users
                .FirstOrDefaultAsync(u => u.PhoneNum == phoneNum);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            using var context = await _factory.CreateDbContextAsync();
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> CheckTableAvailabilityAsync(int tableId, DateTime date)
        {
            // Проверяем, нет ли бронирований на этот стол в диапазоне ±2 часа
            var startTime = date.AddHours(-2);
            var endTime = date.AddHours(2);

            using var context = await _factory.CreateDbContextAsync();
            var existingReservation = await context.Reservations
                .AnyAsync(r => r.IdTable == tableId &&
                              r.Date >= startTime &&
                              r.Date <= endTime);

            return !existingReservation; // true если стол доступен
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Reservations
                .Include(r => r.User)
                .Include(r => r.Table)
                .ThenInclude(t => t.TableType)
                .ToListAsync();
        }
    }
}