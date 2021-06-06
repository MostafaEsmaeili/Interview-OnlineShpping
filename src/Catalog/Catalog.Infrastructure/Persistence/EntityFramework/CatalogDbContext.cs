using Catalog.Application.Common.interfaces;
using Catalog.Domain.entities;
using Catalog.Infrastructure.Persistence.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.EntityFramework
{
    public class CatalogDbContext : DbContext, ICatalogDbContext
    {
        private ICurrentUserService _currentUserService;
        private IDateTime _dateTime;

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        { }

        public DbSet<CategoryEntity> Categories { get;set;}
        public DbSet<ProductEntity> Products { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _dateTime = this.GetService<IDateTime>();
            _currentUserService = this.GetService<ICurrentUserService>();

            foreach (var entry in ChangeTracker.Entries<IHaveAudit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedAt = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        entry.Property(x => x.CreatedAt).IsModified = false;
                        entry.Entity.ModifiedBy = _currentUserService.UserId;
                        entry.Entity.ModifiedAt = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            //todo: raise an evvent
            // await DispatchEvents();

            return result;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }

    }
}
