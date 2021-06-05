using Catalog.Application.Category.Models;
using Catalog.Application.Common.interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Category.Create
{
    public class Validator : AbstractValidator<CreateCategoryDto>
    {
        private readonly ICatalogDbContext _context;
        public Validator(ICatalogDbContext context)
        {
            RuleFor(p => p.Name).NotEmpty();
            When(x => x.ParentId is not null, () =>
            {
                RuleFor(x => x.ParentId).MustAsync(ShouldBeValidCategoryId);

            });
            _context = context;
        }

        private async Task<bool> ShouldBeValidCategoryId(Guid? subId, CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().AnyAsync(x=>x.Id == subId);
        }
    }
}//end namespace
