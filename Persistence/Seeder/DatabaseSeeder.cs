
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Seeder;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        await Seed.EquipmentTypesSeeder(context);
        await Seed.SeedProductionFacilities(context);
    }
}
