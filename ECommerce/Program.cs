
using DomainLayer.Contracts;
using ECommerce.Web.CustomMiddleware;
using ECommerce.Web.Extensions;
using ECommerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using Persistence;
using Persistence.Data.Contexts;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using System.Threading.Tasks;

namespace ECommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            builder.Services.AddControllers();

            builder.Services.AddApplicationApiService();
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationService();


            var app = builder.Build();

            await app.SeedingData();

            app.UseMiddleware<ExceptionHandlerMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
