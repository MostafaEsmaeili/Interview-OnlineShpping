using AutoMapper;
using Catalog.Application.Category.Models;
using Catalog.Application.Product.Models;
using Catalog.Domain.entities;

namespace Catalog.Application.Common.mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryDto, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryResponse>().ForMember(x=>x.ParentName, cfg=>cfg.MapFrom(m=>m.Parent.Name));

            CreateMap<CreateProductDto, ProductEntity>();
        }
    }
}
