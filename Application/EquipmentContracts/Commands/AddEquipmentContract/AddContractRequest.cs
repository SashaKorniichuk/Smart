namespace Application.EquipmentContracts.Commands.AddEquipmentContract;

public sealed record AddContractRequest(Guid ProductionFacilityId, Guid EquipmentTypeId, int Quantity);