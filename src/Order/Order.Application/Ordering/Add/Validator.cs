using FluentValidation;
using Order.Application.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Add
{
    public class Validator: AbstractValidator<AddOrder>
    {
        private readonly ICatalogMicroService _catalogMicroService;
        public Validator(ICatalogMicroService catalogMicroService)
        {
            _catalogMicroService = catalogMicroService;

            RuleFor(x => x.Model).NotNull();
            When(x => x.Model is not null, () =>
            {
                RuleFor(x => x.Model.ProductId).MustAsync(BeValidProduct);


            });
        }
        private async Task<bool> BeValidProduct(Guid productId, CancellationToken cancellationToken)
        {
            return await _catalogMicroService.IsValidProduct(productId, cancellationToken);
        }
    }
}
