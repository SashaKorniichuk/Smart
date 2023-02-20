using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    internal sealed class ProductionFacilityConfiguration : IEntityTypeConfiguration<ProductionFacility>
    {
        public void Configure(EntityTypeBuilder<ProductionFacility> builder)
        {
            builder.ToTable(nameof(ProductionFacility));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.EquipmentArea).IsRequired();
            builder.Property(x => x.Name).IsRequired();

        }
    }
}
