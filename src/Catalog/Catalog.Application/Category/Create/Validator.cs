using Catalog.Application.Category.Models;
using Catalog.Application.Common.interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Category.Create
{
    public class Validator : AbstractValidator<CreateCategory>
    {
        private readonly ICatalogDbContext _context;
        public Validator(ICatalogDbContext context)
        {
            _context = context;
            RuleFor(p => p.Model).NotNull();
            When(x => x.Model is not null, () =>
            {
                RuleFor(p => p.Model.Name).NotEmpty().NotNull();
                When(x => x.Model.ParentId is not null, () =>
                {
                    RuleFor(x => x.Model.ParentId).MustAsync(ShouldBeValidCategoryId);
                });


            });

        }

        private async Task<bool> ShouldBeValidCategoryId(Guid? subId, CancellationToken cancellationToken)
        {
            return await _context.Categories.AsNoTracking().AnyAsync(x => x.Id == subId);
        }
    }
}//end namespace
