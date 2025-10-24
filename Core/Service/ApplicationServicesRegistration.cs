using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection service)
        {

            service.AddScoped<IServiceManager, ServiceManager>();
            service.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(AssemblyReference).Assembly);
            return service;
        }
    }
}
