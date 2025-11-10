public class CartItem
{
    public int IdCartItem { get; set; }
    public int IdCart { get; set; }
    public Cart Cart { get; set; } = null!;
    public int IdDish { get; set; }
    public Dish Dish { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public DateTime AddedAt { get; set; } = DateTime.Now;
    public decimal TotalPrice => Dish.Cost * Quantity;
}
