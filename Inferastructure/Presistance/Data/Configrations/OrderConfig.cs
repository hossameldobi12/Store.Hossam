using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configrations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
         public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne( o => o.Address,a => a.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.paymentStatus).HasConversion( o => o.ToString(), s => Enum.Parse<OrderPaymentStatus>(s) );
            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,4)");
        }
    }
}
