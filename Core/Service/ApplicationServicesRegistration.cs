using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Service.MappingProfiles;
using ServiceAbstraction;
using System.Reflection;
using System.Text;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(AssemblyReference).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            return services;
        }
        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication((config) =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],
                  
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],
                    
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;






        }
    }
}
