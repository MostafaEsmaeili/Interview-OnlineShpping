using AutoMapper.EquivalencyExpression;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Add;
using Order.Application.Common.behaviours;
using Order.Application.Common.mapping;
using System;
using System.Reflection;

namespace Order.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(automapper =>
           {
               automapper.AddCollectionMappers();
           }, typeof(MappingProfile).Assembly);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(AddOrder).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }
    }
}
