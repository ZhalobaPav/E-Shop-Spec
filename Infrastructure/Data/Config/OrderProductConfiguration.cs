using ApplicationCore.Enities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProductDetails>
    {
        public void Configure(EntityTypeBuilder<OrderProductDetails> builder)
        {
            builder.OwnsOne(opd => opd.ProductOrder, po =>
            {
                po.WithOwner();
                po.Property(po=>po.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            builder.Property(po => po.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
