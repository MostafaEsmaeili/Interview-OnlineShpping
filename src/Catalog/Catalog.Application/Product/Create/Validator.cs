using Catalog.Application.Common.interfaces;
using Catalog.Application.Product.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Product.Create
{
    public class Validator : AbstractValidator<CreateProduct>
    {
        private readonly ICatalogDbContext _dbContext;

        public Validator(ICatalogDbContext dbContext)
        {
            RuleFor(x=>x.Model.Name).NotEmpty().NotNull().MaximumLength(128);
            RuleFor(x=>x.Model.Price).GreaterThanOrEqualTo(0);
            RuleFor(x=>x.Model.Description).NotNull().NotEmpty().MaximumLength(512);
            RuleFor(x=>x.Model.CategoryId).MustAsync(ShouldBeValidCategory);
            _dbContext = dbContext;
        }

        private async Task<bool> ShouldBeValidCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.AnyAsync(x=>x.Id == categoryId, cancellationToken);
        }
    }
}
