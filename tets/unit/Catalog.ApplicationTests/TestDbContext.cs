using Catalog.Application.Category.Create;
using Catalog.Application.Common.interfaces;
using Catalog.Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.ApplicationTests
{
    public class TestDbContext : DbContext, ICatalogDbContext
    {
        public DbSet<CategoryEntity> Categories {get;set;}
        public DbSet<ProductEntity> Products { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        { }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in ChangeTracker.Entries<IHaveAudit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "CreatedBytests";
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.ModifiedBy = "Not modified yet";
                        entry.Entity.ModifiedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        entry.Property(x => x.CreatedAt).IsModified = false;
                        entry.Entity.ModifiedBy = "ModifiedByTests";
                        entry.Entity.ModifiedAt = DateTime.Now;
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
            builder.ApplyConfigurationsFromAssembly(typeof(CreateCategory).Assembly);
        }
        public static async Task<TestDbContext> Create(string dbName)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite($"Filename={dbName}.db");
            var ctx = new TestDbContext(options.Options);
            await ctx.Database.EnsureDeletedAsync();
            await ctx.Database.EnsureCreatedAsync();
            await ctx.Database.MigrateAsync();
            return ctx;
        }
    }
    public class TestUnitOfWork : IUnitOfWork
    {
        public ICatalogDbContext DbContext { get;}

        public TestUnitOfWork(ICatalogDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
          return await (DbContext as TestDbContext).SaveChangesAsync(cancellationToken);
        }
    }
}
