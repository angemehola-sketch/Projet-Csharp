using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }
}