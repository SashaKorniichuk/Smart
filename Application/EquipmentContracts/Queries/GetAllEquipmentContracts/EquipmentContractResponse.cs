﻿namespace Application.EquipmentContracts.Queries.GetAllEquipmentContracts;

public sealed record EquipmentContractResponse(Guid Id,string ProductionFacility,string EquipmentType,int EquipmentQuantity);