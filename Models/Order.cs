namespace Restaurant_site.Models;

public class Order
{
    public string OrderNumber { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Apartment { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}
