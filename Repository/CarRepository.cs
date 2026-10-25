using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projet_Csharp.Data;
using Projet_Csharp.InterfaceRepository;

namespace Projet_Csharp.Repository;

public class CarRepository : ICarRepository
{
    private readonly DealershipDbContext _context;

    public CarRepository(DealershipDbContext context)
    {
        _context = context;
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task AddAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }
}