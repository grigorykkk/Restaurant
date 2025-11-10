public class Cart
{
    public int IdCart { get; set; }
    public string? PhoneNum { get; set; }
    public User? User { get; set; }
    public string SessionId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
