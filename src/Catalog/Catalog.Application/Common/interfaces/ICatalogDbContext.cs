using Catalog.Domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Common.interfaces
{
    public interface ICatalogDbContext
    {
        DbSet<CategoryEntity> Categories { get;set;} 
        DbSet<ProductEntity> Products { get;set;}
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
