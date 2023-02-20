using Domain.Primitives;

namespace Domain.Entities;

public sealed class ProductionFacility:Entity
{
    public ProductionFacility(Guid id,string name,double equipmentArea):base(id)
    {
        Name=name;
        EquipmentArea=equipmentArea;
    }
    public string Name { get; private set; }
    public double EquipmentArea { get; private set; }
}