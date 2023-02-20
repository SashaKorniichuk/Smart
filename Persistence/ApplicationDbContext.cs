using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public sealed class ApplicationDbContext:DbContext
{
	public ApplicationDbContext(DbContextOptions options)
		: base(options)
	{

	}

	public DbSet<EquipmentContract> EquipmentContracts { get; set; }

	public DbSet<EquipmentType> EquipmentTypes { get; set;}

	public DbSet<ProductionFacility> ProductionFacilities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}