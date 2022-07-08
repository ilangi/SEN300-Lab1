using Microsoft.EntityFrameworkCore;

public class ItemDb : DbContext
{
    public ItemDb(DbContextOptions<ItemDb> options) : base(options) { }
    public DbSet<Item> Items => Set<Item>();

    public ItemDb()
    {

    }
}