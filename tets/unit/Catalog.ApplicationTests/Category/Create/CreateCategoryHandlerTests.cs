using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Catalog.ApplicationTests;
using Catalog.Application.Common.mapping;
using Catalog.Application.Category.Models;
using Catalog.Application.Category.Create;
using Catalog.Infrastructure.Persistence.EntityFramework;

namespace Catalog.ApplicationTests.Category.Create
{
    public class CreateCategoryHandlerTests
    {
        [Fact()]
        public async Task Can_Create_Category()
        {
            using (var ctx = await TestDbContext.Create(nameof(Can_Create_Category)))
            {
                var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));

                var api = new CreateCategoryDto { 
                    Description ="test",
                    Enabled = true,
                    Name ="c1",
                    ParentId =null
                    };
                var handler = new CreateCategoryHandler(ctx,mapper.CreateMapper(),new TestUnitOfWork(ctx));
                var result = await handler.Handle( new CreateCategory(api), CancellationToken.None);
                Assert.True(result);
            }
        }
    }
}