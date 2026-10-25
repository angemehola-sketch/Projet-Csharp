using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_Csharp.InterfaceRepository;

public interface ICarRepository
{
    Task<List<Car>> GetAllAsync();
    Task AddAsync(Car car);
}