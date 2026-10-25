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
    public void InitDb(List<Customer> clients, List<Car> voitures)
    {
        if (_context.Customers.Any()) return;

        _context.Customers.AddRange(clients);
        _context.Cars.AddRange(voitures);
        _context.SaveChanges();

        AttribuerVoituresVendues();
    }

    #region Attribution des voitures vendues

    // 2 voitures maximum par client
    private void AttribuerVoituresVendues()
    {
        Random hasard = new Random();
        List<Customer> clients = _context.Customers.ToList();
        List<Car> voituresVendues = _context.Cars.Where(c => c.IsSold).ToList();
        Dictionary<Guid, int> compteurs = clients.ToDictionary(c => c.Id, c => 0);

        foreach (Car voiture in voituresVendues)
        {
            List<Customer> disponibles = clients.Where(c => compteurs[c.Id] < 2).ToList();
            if (disponibles.Count == 0) break;

            Customer client = disponibles[hasard.Next(disponibles.Count)];

            _context.Ownerships.Add(new Ownership
            {
                CustomerId = client.Id,
                CarId = voiture.Id,
                SaleDate = DateTime.UtcNow.AddDays(-hasard.Next(0, 14))
            });
            compteurs[client.Id]++;
        }
        _context.SaveChanges();
    }

    #endregion
}