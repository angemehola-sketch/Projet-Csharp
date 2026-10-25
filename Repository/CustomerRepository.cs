using Microsoft.EntityFrameworkCore;
using Projet_Csharp.Data;
using Projet_Csharp.InterfaceRepository;

namespace Projet_Csharp.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly DealershipDbContext _context;

    public CustomerRepository(DealershipDbContext context)
    {
        _context = context;
    }

    public List<Customer> GetAll()
    {
        return _context.Customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
    }

    // Charge un client avec ses voitures
    public Customer? GetWithCars(Guid id)
    {
        return _context.Customers
            .Include(c => c.Ownerships)
            .ThenInclude(o => o.Car)
            .FirstOrDefault(c => c.Id == id);
    }
}