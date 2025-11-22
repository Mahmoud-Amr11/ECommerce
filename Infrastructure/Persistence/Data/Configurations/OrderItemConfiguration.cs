using DomainLayer.Models.OrderModules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public partial class DeliveryMethodConfiguration
    {
        public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
        {
            public void Configure(EntityTypeBuilder<OrderItem> builder)
            {
                builder.Property(oi => oi.Price)
                    .HasColumnType("decimal(8,2)");

                builder.OwnsOne(oi => oi.Product);
            }
        }
    }
}
