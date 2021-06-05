using AutoMapper;
using Catalog.Application.Common.interfaces;
using Catalog.Application.Product.Models;
using Catalog.Domain.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Product.Create
{
    public record CreateProduct(CreateProductDto Model) : IRequest<bool>;
    public class CreateProductHandler : IRequestHandler<CreateProduct, bool>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(ICatalogDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateProduct request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductEntity>(request.Model);
            _context.Products.Add(entity);
            var affectedRow = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return affectedRow > 0;
        }
    }
}
