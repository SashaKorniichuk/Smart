using Domain.Primitives;

namespace Domain.Entities;

public sealed class EquipmentType:Entity
{
    public EquipmentType(Guid id,string name, double area):base(id)
    {
        Name=name;
        Area=area;
    }

    public string Name { get; private set; }
    public double Area { get; private set; }
}