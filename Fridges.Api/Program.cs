<<<<<<< Updated upstream
=======
using Fridges.Application.Interfaces;
using Fridges.Application.Services;
using Fridges.Infrastructure.Extensions;
using Fridges.Persistance.Interfaces;
using Fridges.Persistance.Repositories;
namespace Fridges.Api;
>>>>>>> Stashed changes

namespace Fridges.Api
{
    public class Program
    {
<<<<<<< Updated upstream
        public static void Main(string[] args)
=======
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IFridgeRepository, FridgeRepository>();
        builder.Services.AddScoped<IFridgeService, FridgeService>();
        
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddControllers();
        
        builder.Services.AddOpenApi();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
>>>>>>> Stashed changes
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
