using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.EquipmentContracts.Queries.GetAllEquipmentContracts;

public class GetAllEquipmentContractsQueryHandler : IQueryHandler<GetAllEquipmentContractsQuery, List<EquipmentContractResponse>>
{
    private readonly IEquipmentContractRepository _equipmentContractsRepository;

    public GetAllEquipmentContractsQueryHandler(IEquipmentContractRepository equipmentContractsRepository)
    {
        _equipmentContractsRepository=equipmentContractsRepository;
    }

    public async Task<Result<List<EquipmentContractResponse>>> Handle(GetAllEquipmentContractsQuery request, CancellationToken cancellationToken)
    {
        var contracts = await _equipmentContractsRepository.GetAllEquipmentContractsAsync(cancellationToken);

        var response = contracts.Select(x => new EquipmentContractResponse(x.Id, x.ProductionFacility!.Name, x.EquipmentType!.Name, x.EquipmentQuantity)).ToList();

        return response;
    }
}
