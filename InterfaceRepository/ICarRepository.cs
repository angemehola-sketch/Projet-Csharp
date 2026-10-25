namespace Projet_Csharp.InterfaceRepository;

public interface ICarRepository
{
    List<Car> GetAll();
    List<Car> GetAvailable();
    List<Car> GetSold();
    Car? GetWithOwners(Guid id);
    string Sell(Guid carId, Guid customerId);
    string ChangeOwner(Guid ownershipId, Guid newCustomerId);
    decimal GetWeeklyRevenue();
}