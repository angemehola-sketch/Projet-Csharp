using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projet_Csharp.Data;
using Projet_Csharp.InterfaceRepository;
using Projet_Csharp.Repository;
using Projet_Csharp.Utils;

#region Lancement des services

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Configuration de la base de données PostgreSQL
        services.AddDbContext<DealershipDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));

        // Enregistrement des services utilisés par l'application
        services.AddTransient<DbConnection>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<ICarRepository, CarRepository>();
    })
    .Build();

#endregion

#region Initialisation de la base de données

// Accès aux services
using var scope = host.Services.CreateScope();
IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
DbConnection dbConnection = scope.ServiceProvider.GetRequiredService<DbConnection>();

// Récupération des chemins des fichiers CSV 
string customersPath = Path.Combine(AppContext.BaseDirectory, configuration["Files-csv:Customers"]!);
string carsPath = Path.Combine(AppContext.BaseDirectory, configuration["Files-csv:Cars"]!);

// Import des CSV puis attribution des voitures vendues
dbConnection.InitDb(CsvReader.ReadCustomers(customersPath), CsvReader.ReadCars(carsPath));

#endregion

#region Menu

//Récupération des données utilisés par le menu
ICarRepository cars = scope.ServiceProvider.GetRequiredService<ICarRepository>();
ICustomerRepository customers = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();

//Lancement du menu 
new Menu(cars, customers).Run();

#endregion