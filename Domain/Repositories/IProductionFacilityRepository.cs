namespace Domain.Repositories;

public interface IProductionFacilityRepository
{
    Task<double> GetAreaAsync(Guid productionFacilityId, CancellationToken cancellationToken=default);
}
