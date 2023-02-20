using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class Contracts
    {
        public static readonly Func<double, Error> NotEnoughSpace = area => new Error(
            "Contract.NotEnoughSpace",
            $"There is not enough space in the selected room. Area available: {area} ");

        public static readonly Error Empty = new("Contract.Empty",
            "The list of contracts is empty");
    }
    public static class ProductionFacility
    {
        public static readonly Func<Guid, Error> NotFound = id => new(
            "ProductionFacility.NotFound",
            $"The production facility with Id {id} was not found");
    }

    public static class EquipmentType
    {
        public static readonly Func<Guid, Error> NotFound = id => new(
            "EquipmentType.NotFound",
            $"The equipment type with Id {id} was not found");
    }
}
