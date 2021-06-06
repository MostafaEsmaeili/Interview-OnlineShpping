using Catalog.Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.EntityFramework.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product").HasKey(x=>x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();

            builder.Property(x=>x.Name).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(512).IsRequired();

            builder.Property(x=>x.Price).HasPrecision(18,0);
            builder.Property(x => x.Tags).HasMaxLength(2048);

            builder.HasOne(x=>x.Category).WithMany(x=>x.Products)
                .HasForeignKey(x=>x.CategoryId);

        }
    }
}
