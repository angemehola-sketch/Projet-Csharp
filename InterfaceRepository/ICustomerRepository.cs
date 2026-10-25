using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projet_Csharp.InterfaceRepository;

public interface ICustomerRepository
{
    List<Customer> GetAll();
    Customer? GetWithCars(Guid id);

}