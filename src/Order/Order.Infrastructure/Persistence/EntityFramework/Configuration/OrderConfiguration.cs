using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.entities;
using Order.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence.EntityFramework.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Order").HasKey(x=>x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
            builder.Property(x=>x.UserId).IsRequired().HasMaxLength(36);
            builder.Property(x => x.ProductId).IsRequired().HasMaxLength(36);
            builder.Property(x => x.Quantity).IsRequired();
            builder.SetAuditConfiguration();

        }
    }
}
