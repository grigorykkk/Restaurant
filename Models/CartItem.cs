namespace Restaurant_site.Models;

public class CartItem
{
    public MenuItem MenuItem { get; set; } = null!;
    public int Quantity { get; set; }

    public decimal TotalPrice => MenuItem.Price * Quantity;
}
