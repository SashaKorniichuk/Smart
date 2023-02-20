using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class EquipmentTypeRepository : IEquipmentTypeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EquipmentTypeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<double> GetAreaByIdAsync(Guid equipmentTypeId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<EquipmentType>()
              .Where(pf => pf.Id == equipmentTypeId)
              .Select(pf => pf.Area)
              .FirstOrDefaultAsync(cancellationToken);
    }
}
