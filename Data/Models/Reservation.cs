public class Reservation
{
    public int IdReserv { get; set; }
    public string PhoneNum { get; set; } = string.Empty;
    public User User { get; set; } = null!;
    public DateTime Date { get; set; }
    public int GuestCount { get; set; }
    public int IdTable { get; set; }
    public Table Table { get; set; } = null!;
}