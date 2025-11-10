public class Dish
{
    public int IdDish { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public int IdDishType { get; set; }
    public DishType DishType { get; set; } = null!;

    public List<DishInOrder> DishInOrders { get; set; } = new();
}