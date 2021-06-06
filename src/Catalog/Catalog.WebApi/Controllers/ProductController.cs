using Catalog.Application.Common.Paging;
using Catalog.Application.Product.Create;
using Catalog.Application.Product.GetById;
using Catalog.Application.Product.Models;
using Catalog.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.WebApi.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateProductAsync([FromBody]CreateProductDto request)
        {
            return (await _mediator.Send(new CreateProduct(request))).ToJsonResult();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            return (await _mediator.Send(new GetProductById(id))).ToJsonResult();
        }
    }
}
