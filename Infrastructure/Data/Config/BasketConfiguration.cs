using ApplicationCore.Enities.Basket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Basket.BasketProducts));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(b=>b.BuyerId)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
