using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    :base(options) {}

    public DbSet<Product> Product=> Set<Product>();
      public DbSet<Experience> Experience => Set<Experience>();

      public DbSet<Users> Users =>  Set<Users>();
      public DbSet<AdminModel> Admin => Set<AdminModel>();
    }