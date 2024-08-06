using ApplicationCore.Enities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderDetails));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(b => b.BuyerId)
                .IsRequired()
                .HasMaxLength(256);
            builder.OwnsOne(o => o.Address, a =>
            {
                a.WithOwner();
                a.Property(a => a.ZipCode)
                .HasMaxLength(18)
                .IsRequired();

                a.Property(a => a.Street)
                    .HasMaxLength(180)
                    .IsRequired();

                a.Property(a => a.State)
                    .HasMaxLength(60);

                a.Property(a => a.Country)
                    .HasMaxLength(90)
                    .IsRequired();

                a.Property(a => a.City)
                    .HasMaxLength(100)
                    .IsRequired();
            });
            builder.Navigation(x => x.Address).IsRequired();
        }
    }
}
