using Microsoft.EntityFrameworkCore;

public class OrdersDb : DbContext
{
    public OrdersDb(DbContextOptions<OrdersDb> options) : base(options) { }
    public DbSet<Checkout> Checkout => Set<Checkout>();
    public OrdersDb()
    {

    }
} 