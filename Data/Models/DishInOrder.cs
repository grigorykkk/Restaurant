public class DishInOrder
{
    public int IdDish { get; set; }
    public Dish Dish { get; set; } = null!;
    public int IdOrder { get; set; }
    public Order Order { get; set; } = null!;
}