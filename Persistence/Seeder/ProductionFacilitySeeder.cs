using Domain.Entities;

namespace Persistence.Seeder
{
    internal static partial class Seed
    {
        public static async Task SeedProductionFacilities(ApplicationDbContext _context)
        {
            if (!_context.ProductionFacilities.Any())
            {
                var productionFacilities = new List<ProductionFacility>
                {
                    new ProductionFacility(Guid.NewGuid(), "ACME Corp", 1000),
                    new ProductionFacility(Guid.NewGuid(), "Globe Corporation", 2000),
                    new ProductionFacility(Guid.NewGuid(), "Umbrella Corporation", 1500),
                    new ProductionFacility(Guid.NewGuid(), "Wayne Enterprises", 5000),
                    new ProductionFacility(Guid.NewGuid(), "Stark Industries", 3000)
                };

                await _context.ProductionFacilities.AddRangeAsync(productionFacilities);
                await _context.SaveChangesAsync();
            }
        }
    }
}
