using ECommerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static IServiceCollection AddApplicationApiService(this IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;

            });
            return service;
        }
    }
}
