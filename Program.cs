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
    .ConfigureServices((contexte, services) =>
    {
        services.AddDbContext<DealershipDbContext>(options =>
            options.UseNpgsql(contexte.Configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<DbConnection>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<ICarRepository, CarRepository>();
    })
    .Build();

#endregion

#region Initialisation de la base de données

using var scope = host.Services.CreateScope();
IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
DbConnection dbConnection = scope.ServiceProvider.GetRequiredService<DbConnection>();

string cheminClients = Path.Combine(AppContext.BaseDirectory, configuration["Files-csv:Clients"]!);
string cheminVoitures = Path.Combine(AppContext.BaseDirectory, configuration["Files-csv:Voitures"]!);

dbConnection.InitDb(CsvLecteur.LireClients(cheminClients), CsvLecteur.LireVoitures(cheminVoitures));

#endregion

Console.WriteLine("Initialisation terminée.");