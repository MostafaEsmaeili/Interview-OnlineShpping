using Catalog.Application.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICatalogDbContext DbContext {get; }

        public UnitOfWork(ICatalogDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           return (DbContext as CatalogDbContext).SaveChangesAsync(cancellationToken);
        }
    }
}
