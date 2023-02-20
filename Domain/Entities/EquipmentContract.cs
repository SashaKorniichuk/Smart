using Domain.Primitives;

namespace Domain.Entities;

public sealed class EquipmentContract : Entity
{
    public EquipmentContract(Guid id, Guid productionFacilityId, Guid equipmentTypeId,int equipmentQuantity):base(id)
    {
        ProductionFacilityId=productionFacilityId;
        EquipmentTypeId=equipmentTypeId;
        EquipmentQuantity=equipmentQuantity;
    }
    public Guid ProductionFacilityId { get; private set; }
    public ProductionFacility? ProductionFacility { get; private set; }
    public Guid EquipmentTypeId { get; private set; }
    public EquipmentType? EquipmentType { get; private set; }
    public int EquipmentQuantity { get; private set; }
}
