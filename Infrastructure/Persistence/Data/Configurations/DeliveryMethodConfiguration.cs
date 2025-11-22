using DomainLayer.Models.OrderModules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public partial class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price)
                .HasColumnType("decimal(8,2)");

            builder.Property(d => d.ShortName)
                .HasColumnType("varchar(50)");

            builder.Property(d => d.Description)
              .HasColumnType("varchar(100)");

            builder.Property(d => d.DeliveryTime)
                .HasColumnType("varchar(50)");

        }
    }
}
