
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data.Contexts;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;

namespace ECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IServiceManager, ServiceManager>();




            builder.Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()),typeof(AssemblyReference).Assembly);

            var app = builder.Build();


           var Scope= app.Services.CreateScope();
           var dataSeeding=Scope.ServiceProvider.GetRequiredService<IDataSeeding>();


            dataSeeding.SeedDataAsync();


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
