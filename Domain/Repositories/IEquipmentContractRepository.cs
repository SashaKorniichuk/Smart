using Domain.Entities;

namespace Domain.Repositories;

public interface IEquipmentContractRepository
{
    Task<IEnumerable<EquipmentContract>> GetAllEquipmentContractsAsync(CancellationToken cancellationToken = default);
    Task AddAsync(EquipmentContract contract, CancellationToken cancellationToken = default);
    Task<double> GetUsedSpaceAsync(Guid productionFacilityId, CancellationToken cancellationToken = default);
}
