using AutoMapper;
using Catalog.Application.Category.Models;
using Catalog.Application.Common.Extensions;
using Catalog.Application.Common.interfaces;
using Catalog.Application.Common.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Category.GetAll
{
    public record GetCategoriesByFilter(CategoryRequestFilter Filter) : IRequest<PagedCollection<CategoryResponse>>;
    public class GetCategoriesByFilterHandler : IRequestHandler<GetCategoriesByFilter, PagedCollection<CategoryResponse>>
    {
        private readonly ICatalogDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCategoriesByFilterHandler(ICatalogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagedCollection<CategoryResponse>> Handle(GetCategoriesByFilter request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Categories.Include(x=>x.Parent).AsNoTracking();
            if (!request.Filter.Name.IsNullOrEmpty())
            {
                query = query.Where(x => x.Name == request.Filter.Name);
            }
            if (request.Filter.Enable is not null)
            {
                query = query.Where(x => x.Enabled == request.Filter.Enable);
            }
            var total = await query.CountAsync();
            query = query.SetOrderingAndPaging(request.Filter);
            var result = await _mapper.ProjectTo<CategoryResponse>(query).ToListAsync();

            return new PagedCollection<CategoryResponse>(result,
                                                             total,
                                                             request.Filter.Page);
        }
    }
}
