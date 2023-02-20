using Domain.Entities;

namespace Persistence.Seeder
{
    internal static partial class Seed
    {
        public static async Task EquipmentTypesSeeder(ApplicationDbContext _context)
        {
            if (!_context.EquipmentTypes.Any())
            {
                var equipmentTypes = new List<EquipmentType>
                {
                    new EquipmentType(Guid.NewGuid(), "CNC Machine", 50),
                    new EquipmentType(Guid.NewGuid(), "3D Printer", 20),
                    new EquipmentType(Guid.NewGuid(), "Lathe", 30),
                    new EquipmentType(Guid.NewGuid(), "Milling Machine", 40),
                    new EquipmentType(Guid.NewGuid(), "Welding Machine", 25)
                };

                await _context.EquipmentTypes.AddRangeAsync(equipmentTypes);
                await _context.SaveChangesAsync();
            }
        }
    }
}
