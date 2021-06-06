using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Order.Application.Common.interfaces;
using Order.Domain.entities;
using Order.Infrastructure.Persistence.EntityFramework.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence.EntityFramework
{
    public class OrderDbContext : DbContext, IOrderDbContext
    {
        private ICurrentUserService _currentUserService;
        private IDateTime _dateTime;

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        { }


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
            builder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        }

    }
}
