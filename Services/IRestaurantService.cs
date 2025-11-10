using Restaurant_site.Models;

namespace Restaurant_site.Services
{
    public interface IRestaurantService
    {
        Task<List<Dish>> GetDishesAsync();
        Task<List<Table>> GetTablesAsync();
        Task<List<TableType>> GetTableTypesAsync();
        Task<List<DishType>> GetDishTypesAsync();
        Task<bool> CreateReservationAsync(Reservation reservation);
        Task<bool> CreateOrderAsync(Order order);
        Task<User?> GetUserByPhoneAsync(string phoneNum);
        Task<User> CreateUserAsync(User user);
    }
}