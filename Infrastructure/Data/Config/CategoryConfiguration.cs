using ApplicationCore.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(ci => ci.Id)
                .UseHiLo("category_hilo")
                .IsRequired();
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50); 
        }
    }
}
