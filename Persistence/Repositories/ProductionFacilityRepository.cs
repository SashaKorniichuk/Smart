using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductionFacilityRepository:IProductionFacilityRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductionFacilityRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<double> GetAreaAsync(Guid productionFacilityId, CancellationToken cancellationToken = default)
    {
         return await _dbContext.Set<ProductionFacility>()
            .Where(pf => pf.Id == productionFacilityId)
            .Select(pf => pf.EquipmentArea)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
