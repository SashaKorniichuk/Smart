using FluentValidation;

namespace Application.EquipmentContracts.Commands.AddEquipmentContract;

internal class AddEquipmentContractCommandValidation : AbstractValidator<AddEquipmentContractCommand>
{
    public AddEquipmentContractCommandValidation()
    {
        RuleFor(x => x.EquipmentTypeId).NotEmpty();

        RuleFor(x => x.ProductionFacilityId).NotEmpty();

        RuleFor(x => x.EquipmentQuantity).NotEmpty().GreaterThan(0);
    }
}

