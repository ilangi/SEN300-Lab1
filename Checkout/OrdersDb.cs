using Microsoft.EntityFrameworkCore;

public class OrdersDb : DbContext
{
    public OrdersDb(DbContextOptions<OrdersDb> options) : base(options) { }
    public DbSet<PersonalOrderInformation> Checkout => Set<PersonalOrderInformation>();
    public DbSet<Item> Cart => Set<Item>();
    public OrdersDb()
    {

    }
} 