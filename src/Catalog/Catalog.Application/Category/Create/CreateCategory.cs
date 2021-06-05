using AutoMapper;
using Catalog.Application.Category.Models;
using Catalog.Application.Common.interfaces;
using Catalog.Domain.entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Category.Create
{
    public record CreateCategory(CreateCategoryDto Model) : IRequest<bool>;
    public class CreateOrUpdateCategoryHandler : IRequestHandler<CreateCategory, bool>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateCategoryHandler(ICatalogDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CategoryEntity>(request.Model);
            _context.Categories.Add(entity);
            var affectedRow = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return affectedRow > 0;
        }
    }
}//end namespace
