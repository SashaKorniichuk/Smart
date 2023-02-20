namespace Domain.Repositories; 

public interface IEquipmentTypeRepository
{
    Task<double> GetAreaByIdAsync(Guid equipmentTypeId, CancellationToken cancellationToken = default);
}
