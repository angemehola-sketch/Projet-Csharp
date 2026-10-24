using Microsoft.EntityFrameworkCore;
using Projet_Csharp.Models;

namespace Projet_Csharp.Data;

/// <summary>
/// Contexte EF Core : fait le lien entre les classes C# et les tables PostgreSQL.
/// </summary>
public class DealershipContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Sale> Sales => Set<Sale>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Chaîne de connexion : variable d'environnement, sinon valeurs du docker-compose.yml
        string connection = Environment.GetEnvironmentVariable("CONCESSION_DB")
                            ?? "Host=localhost;Port=6432;Database=car_dealership;Username=dealership_user;Password=Cars_2026";
        options.UseNpgsql(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Les identifiants Guid sont créés par le code C#, jamais par la base
        modelBuilder.Entity<Customer>().Property(c => c.Id).ValueGeneratedNever();
        modelBuilder.Entity<Car>().Property(c => c.Id).ValueGeneratedNever();
        modelBuilder.Entity<Sale>().Property(s => s.Id).ValueGeneratedNever();

        // Un courriel = un client
        modelBuilder.Entity<Customer>().HasIndex(c => c.Email).IsUnique();

        // Prix avec 2 décimales
        modelBuilder.Entity<Car>().Property(c => c.PriceExclTax).HasPrecision(10, 2);
        modelBuilder.Entity<Sale>().Property(s => s.PriceExclTax).HasPrecision(10, 2);

        // Une voiture ne se vend qu'une fois
        modelBuilder.Entity<Sale>().HasIndex(s => s.CarId).IsUnique();

        // Plusieurs-à-plusieurs : EF Core crée seul la table d'association
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Customers)
            .WithMany(c => c.Cars);
    }
}