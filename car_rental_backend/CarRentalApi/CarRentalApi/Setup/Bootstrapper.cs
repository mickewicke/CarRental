using CarRentalApi.Services;
using CarRentalDbModel.DbModels;
//using Microsoft.EntityFrameworkCore.Relational.Specification.Tests;
using Microsoft.Data.Sqlite;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CarRentalApi.Services.Price;
using CarRentalApi.Repositories;
using CarRentalApi.Services.RentalService;

namespace CarRentalApi.Setup
{
    public class Bootstrapper
    {
        public static void ConfigureCommon(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<PriceService>();
            services.AddTransient<RentalService>();
        }

        public static void ConfigureProd(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "Server=DESKTOP-5F2H5NP;Database=CarRental;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            services.AddDbContext<CarRentalContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<ISystemClock, SystemClock>();
            services.AddCors();
        }
    }
}