using FluentValidation;
using Fridges.Api.Extensions;
using Fridges.Application.DTOs;
using Fridges.Application.Validators.Fridges;
using Fridges.Application.Validators.Products;
using Fridges.Application.Validators.Recipes;
using Fridges.Infrastructure.Extensions;

namespace Fridges.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
       
        builder.Services.AddServices();

        builder.Services.AddDatabase(builder.Configuration);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi();

        var app = builder.Build();

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
