using Microsoft.EntityFrameworkCore;


namespace Projet_Csharp.Data;

public class DealershipDbContext : DbContext
{
    public DealershipDbContext(DbContextOptions<DealershipDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Ownership> Ownerships { get; set; }
}