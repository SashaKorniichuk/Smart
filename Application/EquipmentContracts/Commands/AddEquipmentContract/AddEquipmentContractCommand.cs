using Application.Abstractions.Messaging;

namespace Application.EquipmentContracts.Commands.AddEquipmentContract;

public sealed record AddEquipmentContractCommand(Guid ProductionFacilityId, Guid EquipmentTypeId, int EquipmentQuantity) : ICommand;