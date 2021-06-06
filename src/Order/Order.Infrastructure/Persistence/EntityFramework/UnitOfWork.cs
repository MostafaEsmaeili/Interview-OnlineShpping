using Order.Application.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public IOrderDbContext DbContext { get; }

        public UnitOfWork(IOrderDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return (DbContext as OrderDbContext).SaveChangesAsync(cancellationToken);
        }
    }
}
