using AutoMapper.EquivalencyExpression;
using Catalog.Application.Common.behaviours;
using Catalog.Application.Common.mapping;
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
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
