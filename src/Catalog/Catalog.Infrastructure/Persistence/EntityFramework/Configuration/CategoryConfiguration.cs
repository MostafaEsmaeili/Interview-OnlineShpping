using Catalog.Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.EntityFramework.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(512);
            builder.HasOne(x=>x.Parent).WithMany(x=>x.SubCategories).HasForeignKey(x=>x.ParentId).IsRequired(false);
        }
    }
}
