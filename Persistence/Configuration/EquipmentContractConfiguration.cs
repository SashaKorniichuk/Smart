using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    internal sealed class EquipmentContractConfiguration : IEntityTypeConfiguration<EquipmentContract>
    {
        public void Configure(EntityTypeBuilder<EquipmentContract> builder)
        {
            builder.ToTable(nameof(EquipmentContract));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.EquipmentQuantity).IsRequired();
            builder.Property(x => x.EquipmentTypeId).IsRequired();
            builder.Property(x => x.ProductionFacilityId).IsRequired();
        }
    }
}
