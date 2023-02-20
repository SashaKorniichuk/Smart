using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class EquipmentContractRepository:IEquipmentContractRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EquipmentContractRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(EquipmentContract contract,CancellationToken cancellationToken=default)
    {
       await _dbContext.Set<EquipmentContract>().AddAsync(contract,cancellationToken);
    }

    public async Task<IEnumerable<EquipmentContract>> GetAllEquipmentContractsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<EquipmentContract>()
            .Include(x=>x.EquipmentType)
            .Include(x=>x.ProductionFacility)
            .ToListAsync(cancellationToken);
    }

    public async Task<double> GetUsedSpaceAsync(Guid productionFacilityId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<EquipmentContract>()
            .Where(ep => ep.ProductionFacilityId == productionFacilityId && ep.EquipmentType != null)
            .SumAsync(ep => ep.EquipmentQuantity * ep.EquipmentType!.Area, cancellationToken);
    }
}