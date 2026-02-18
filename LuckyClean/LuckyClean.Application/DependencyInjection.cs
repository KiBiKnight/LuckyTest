using FluentValidation;
using LuckyClean.Application.Interfaces;
using LuckyClean.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyClean.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
