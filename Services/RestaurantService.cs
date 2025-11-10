using Restaurant_site.Data;
using Microsoft.EntityFrameworkCore;

namespace Restaurant_site.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext _context;

        public RestaurantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            return await _context.Dishes
                .Include(d => d.DishType)
                .ToListAsync();
        }

        public async Task<List<Table>> GetTablesAsync()
        {
            return await _context.Tables
                .Include(t => t.TableType)
                .ToListAsync();
        }

        public async Task<List<TableType>> GetTableTypesAsync()
        {
            return await _context.TableTypes.ToListAsync();
        }

        public async Task<List<DishType>> GetDishTypesAsync()
        {
            return await _context.DishTypes.ToListAsync();
        }

        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            try
            {
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
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
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<User?> GetUserByPhoneAsync(string phoneNum)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNum == phoneNum);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> CheckTableAvailabilityAsync(int tableId, DateTime date)
        {
            // Проверяем, нет ли бронирований на этот стол в диапазоне ±2 часа
            var startTime = date.AddHours(-2);
            var endTime = date.AddHours(2);

            var existingReservation = await _context.Reservations
                .AnyAsync(r => r.IdTable == tableId &&
                              r.Date >= startTime &&
                              r.Date <= endTime);

            return !existingReservation; // true если стол доступен
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Table)
                .ThenInclude(t => t.TableType)
                .ToListAsync();
        }
    }
}