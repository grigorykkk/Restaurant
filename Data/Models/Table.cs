public class Table
{
    public int IdTable { get; set; }
    public int IdType { get; set; }
    public TableType TableType { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public int SeatsCount { get; set; }

    public List<Reservation> Reservations { get; set; } = new();
}