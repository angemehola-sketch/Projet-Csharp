using Projet_Csharp.Data;

namespace Projet_Csharp.InterfaceRepository;

public class DbConnection
{
    private readonly DealershipDbContext _context;

    public DbConnection(DealershipDbContext context)
    {
        _context = context;
    }

    // Importe les données une seule fois, puis attribue les voitures déjà vendues
    public void InitDb(List<Customer> customers, List<Car> cars)
    {
        // Si des clients existent déjà, l'import a déjà été fait
        if (_context.Customers.Any())
        {
            return;
        }

        // Ajout des clients et des voitures en base
        _context.Customers.AddRange(customers);
        _context.Cars.AddRange(cars);
        _context.SaveChanges();

        // Attribution des voitures déjà vendues à des clients
        AssignSoldCars();
    }

    #region Attribution des voitures vendues

    // 2 voitures maximum par client
    private void AssignSoldCars()
    {
        Random random = new Random();

        // Récupération des clients et des voitures vendues depuis la base
        List<Customer> customers = _context.Customers.ToList();
        List<Car> soldCars = _context.Cars.Where(c => c.IsSold).ToList();
        // Nombre de voitures déjà attribuées à chaque client avec 0 au départ
        Dictionary<Guid, int> carCounts = customers.ToDictionary(c => c.Id, c => 0);

        foreach (Car car in soldCars)
        {
            // Seuls les clients qui ont moins de 2 voitures peuvent en recevoir une
            List<Customer> available = customers.Where(c => carCounts[c.Id] < 2).ToList();
            if (available.Count == 0) break;

            // Choix d'un client au hasard parmi les disponibles
            Customer customer = available[random.Next(available.Count)];

            // Création de la propriété, avec une date de vente dans les 14 derniers jours
            Ownership ownership = new Ownership();
            ownership.CustomerId = customer.Id;
            ownership.CarId = car.Id;
            ownership.SaleDate = DateTime.UtcNow.AddDays(-random.Next(0, 14));
            _context.Ownerships.Add(ownership);

            // Le client a une voiture de plus
            carCounts[customer.Id]++;
        }
        _context.SaveChanges();
    }

    #endregion
}