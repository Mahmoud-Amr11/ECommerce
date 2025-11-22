using DomainLayer.Models.OrderModules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           builder.Property(o=>o.SubTotal)
                .HasColumnType("decimal(8,2)");

            builder.HasMany(o => o.Items)
                .WithOne();

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId);

            builder.OwnsOne(o => o.Address);

        }
    }
}
