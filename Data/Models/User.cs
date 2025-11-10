public class User
{
    public string PhoneNum { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<Order> Orders { get; set; } = new();
    public List<Reservation> Reservations { get; set; } = new();
}