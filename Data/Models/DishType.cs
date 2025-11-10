public class DishType
{
    public int IdDishType { get; set; }
    public string Title { get; set; } = string.Empty;

    public List<Dish> Dishes { get; set; } = new();
}