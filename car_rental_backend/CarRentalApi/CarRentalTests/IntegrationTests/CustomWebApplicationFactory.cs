using CarRentalApi.Models;
using CarRentalDbModel.DbModels;
using CarRentalTests.MockClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<ISystemClock, MockSystemClock>();
            services.AddDbContext<CarRentalContext>(options =>
            {
                options.UseSqlite("DataSource=CarRental;Mode=Memory;Cache=Shared");
            }).BuildServiceProvider();
        }
       );
    }
}