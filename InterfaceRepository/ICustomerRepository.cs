using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projet_Csharp.InterfaceRepository;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
}