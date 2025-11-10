public class Order
{
    public int IdOrder { get; set; }
    public string PhoneNum { get; set; } = string.Empty;
    public User User { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.Now;
    public string Address { get; set; } = string.Empty;

    public List<DishInOrder> DishInOrders { get; set; } = new();
}