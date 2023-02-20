using Application.Abstractions.Messaging;

namespace Application.EquipmentContracts.Queries.GetAllEquipmentContracts;

public sealed record GetAllEquipmentContractsQuery():IQuery<List<EquipmentContractResponse>>;