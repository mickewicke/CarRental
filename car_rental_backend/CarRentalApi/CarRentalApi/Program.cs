using CarRentalApi.API;
using CarRentalApi.Services;
using CarRentalApi.Setup;
using CarRentalDbModel.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle = CarRentalDbModel.DbModels.Vehicle;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        Bootstrapper.ConfigureCommon(builder.Services, builder.Configuration);


        if (builder.Environment.EnvironmentName != "IntegrationTests")
        {
            Bootstrapper.ConfigureProd(builder.Services, builder.Configuration);
        }
        
        var app = builder.Build();
        RentalApi.MapEndpoints(app);

        app.UseCors(builder => builder
 .WithOrigins("localhost:3000")
 .AllowAnyMethod()
 .AllowAnyHeader()
);
        app.Run();

    }


}


