using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<TableType> TableTypes { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DishType> DishTypes { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<DishInOrder> DishInOrders { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TableType>()
            .HasKey(tt => tt.IdTableType);

        modelBuilder.Entity<Table>()
            .HasKey(t => t.IdTable);

        modelBuilder.Entity<User>()
            .HasKey(u => u.PhoneNum);

        modelBuilder.Entity<DishType>()
            .HasKey(dt => dt.IdDishType);

        modelBuilder.Entity<Dish>()
            .HasKey(d => d.IdDish);

        modelBuilder.Entity<Order>()
            .HasKey(o => o.IdOrder);

        modelBuilder.Entity<Reservation>()
            .HasKey(r => r.IdReserv);

        modelBuilder.Entity<DishInOrder>()
            .HasKey(dio => new { dio.IdDish, dio.IdOrder });

        modelBuilder.Entity<Table>()
            .HasOne(t => t.TableType)
            .WithMany(tt => tt.Tables)
            .HasForeignKey(t => t.IdType);

        modelBuilder.Entity<Dish>()
            .HasOne(d => d.DishType)
            .WithMany(dt => dt.Dishes)
            .HasForeignKey(d => d.IdDishType);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.PhoneNum);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Table)
            .WithMany(t => t.Reservations)
            .HasForeignKey(r => r.IdTable);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.PhoneNum);

        modelBuilder.Entity<DishInOrder>()
            .HasOne(dio => dio.Dish)
            .WithMany(d => d.DishInOrders)
            .HasForeignKey(dio => dio.IdDish);

        modelBuilder.Entity<DishInOrder>()
            .HasOne(dio => dio.Order)
            .WithMany(o => o.DishInOrders)
            .HasForeignKey(dio => dio.IdOrder);

        modelBuilder.Entity<Cart>()
            .HasKey(c => c.IdCart);

        modelBuilder.Entity<CartItem>()
            .HasKey(ci => ci.IdCartItem);

        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.PhoneNum)
            .IsRequired(false);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.IdCart);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Dish)
            .WithMany()
            .HasForeignKey(ci => ci.IdDish);
    }
}