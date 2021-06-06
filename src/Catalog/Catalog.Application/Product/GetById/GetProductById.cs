using AutoMapper;
using Catalog.Application.Common.interfaces;
using Catalog.Application.Product.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Product.GetById
{
    public record GetProductById(Guid Id) : IRequest<ProductResponse>;
    public class GetProductByIdHandler : IRequestHandler<GetProductById, ProductResponse>
    {
        private readonly ICatalogDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(ICatalogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(GetProductById request, CancellationToken cancellationToken)
        {
           var query=await _dbContext.Products.FromSqlInterpolated($"dbo.GetProductById {request.Id} ").ToListAsync();
            return  _mapper.ProjectTo<ProductResponse>(query.AsQueryable()).SingleOrDefault();
        }
    }
}
