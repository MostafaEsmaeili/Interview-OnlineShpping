using AutoMapper.EquivalencyExpression;
using Catalog.Application.Common.behaviours;
using Catalog.Application.Common.mapping;
using Catalog.Application.Product.Create;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Catalog.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper( automapper =>
            {
                automapper.AddCollectionMappers();
            }, typeof(MappingProfile).Assembly);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CreateProduct).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }
    }
}
