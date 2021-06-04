using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Common.interfaces
{
    public interface IUnitOfWork
    {
        public  ICatalogDbContext DbContext {get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
