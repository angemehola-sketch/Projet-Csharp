using Microsoft.EntityFrameworkCore;
using Projet_Csharp.Data;
using Projet_Csharp.InterfaceRepository;

namespace Projet_Csharp.Repository;

public class CarRepository : ICarRepository
{
	private const int MaxCarsPerCustomer = 2;
	private readonly DealershipDbContext _context;

	public CarRepository(DealershipDbContext context)
	{
		_context = context;
	}

	#region Consultation

	public List<Car> GetAll()
	{
		return _context.Cars.OrderBy(c => c.Brand).ThenBy(c => c.Model).ToList();
	}

	public List<Car> GetAvailable()
	{
		return _context.Cars.Where(c => !c.IsSold).OrderBy(c => c.Brand).ThenBy(c => c.Model).ToList();
	}

	public List<Car> GetSold()
	{
		return _context.Cars.Where(c => c.IsSold).OrderBy(c => c.Brand).ThenBy(c => c.Model).ToList();
	}

	// Charge une voiture avec ses propriétaires
	public Car? GetWithOwners(Guid id)
	{
		return _context.Cars
			.Include(c => c.Ownerships)
			.ThenInclude(o => o.Customer)
			.FirstOrDefault(c => c.Id == id);
	}

	#endregion

	#region Vente et changement de propriétaire

	// Vend une voiture à un client, seulement si elle n'est pas déjà vendue
	public string Sell(Guid carId, Guid customerId)
	{
		Car? car = _context.Cars.Find(carId);
		if (car == null) return "Voiture introuvable.";
		if (car.IsSold) return "Cette voiture est déjà vendue.";
		if (CountCars(customerId) >= MaxCarsPerCustomer)
			return "Ce client possède déjà 2 voitures.";

		car.IsSold = true;

        // Création de la propriété qui lie la voiture au client
        Ownership ownership = new Ownership();
        ownership.CustomerId = customerId;
        ownership.CarId = carId;
        ownership.SaleDate = DateTime.UtcNow;
        _context.Ownerships.Add(ownership);
        // Enregistrement en base
        _context.SaveChanges();
		return "Vente enregistrée.";
	}

	// Transfère une voiture déjà vendue vers un autre client
	public string ChangeOwner(Guid ownershipId, Guid newCustomerId)
	{
		Ownership? ownership = _context.Ownerships.Find(ownershipId);
		if (ownership == null) return "Propriété introuvable.";

		bool alreadyOwner = _context.Ownerships
			.Any(o => o.CarId == ownership.CarId && o.CustomerId == newCustomerId);
		if (alreadyOwner) return "Ce client possède déjà cette voiture.";
		if (CountCars(newCustomerId) >= MaxCarsPerCustomer)
			return "Ce client possède déjà 2 voitures.";

		ownership.CustomerId = newCustomerId;
		_context.SaveChanges();
		return "Changement de propriétaire enregistré.";
	}

	#endregion

	#region Gains

	// Somme des prix HT des voitures vendues sur les 7 derniers jours
	public decimal GetWeeklyRevenue()
	{
		DateTime limit = DateTime.UtcNow.AddDays(-7);
		return _context.Ownerships
			.Where(o => o.SaleDate >= limit)
			.Select(o => o.Car.PriceExclTax)
			.Sum();
	}

	#endregion

	// Nombre de voitures possédées par un client
	private int CountCars(Guid customerId)
	{
		return _context.Ownerships.Count(o => o.CustomerId == customerId);
	}
}