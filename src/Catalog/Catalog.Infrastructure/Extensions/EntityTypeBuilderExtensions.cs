using Catalog.Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Extensions
{
    internal static class EntityTypeBuilderExtensions
    {
        public static void SetAuditConfiguration<T>(this EntityTypeBuilder<T> builder) where T : class, IHaveAudit
        {
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("Datetime");
            builder.Property(x => x.ModifiedAt).IsRequired(false).HasColumnType("Datetime");
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(128).IsUnicode();
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(128).IsUnicode();
        }
    }
}
