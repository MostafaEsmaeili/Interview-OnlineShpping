using Catalog.Application.Category.Create;
using Catalog.Application.Category.GetAll;
using Catalog.Application.Category.Models;
using Catalog.Application.Common.Paging;
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
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateCategoryAsync([FromBody]CreateCategoryDto request)
        {
          return  (await _mediator.Send(new CreateCategory(request))).ToJsonResult();
        }
        [HttpGet]
        public async Task<ActionResult<PagedCollection<CategoryResponse>>> CreateCategoryAsync([FromQuery] CategoryRequestFilter filter)
        {
            return (await _mediator.Send(new GetCategoriesByFilter(filter))).ToJsonResult();
        }
    }
}
