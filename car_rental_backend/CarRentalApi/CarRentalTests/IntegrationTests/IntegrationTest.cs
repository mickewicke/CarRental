
using CarRentalApi.API;
using CarRentalApi.Models;
using CarRentalApi.Repositories;
using CarRentalApi.Services.Price;
using CarRentalDbModel.DbModels;
using CarRentalTests.MockClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace CarRentalTests.IntegrationTests
{
    [Collection("IntegrationTests")]
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        public CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        public IntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();



        }

        [Fact]
        public async void DoRental()
        {
            using var scope = _factory.Services.CreateScope();
            SeedDb(scope.ServiceProvider.GetService<CarRentalContext>());
            var systemClock = (MockSystemClock)scope.ServiceProvider.GetService<ISystemClock>();
         
            systemClock.SetSystemTime(DateTime.Parse("2024-12-01 12:00"));

            var bookingNumber = "A booking number";

            var noOfDays = 5;
            var noOfKm = 100;
            await _client.PostAsJsonAsync("/pickup-vehicle", new PickupVehicleReq
            {
                CarID = 2,
                BookingNumber = bookingNumber,
                CustomerSSN = "1234567890",
                MeterReadingKm = 100
            });

            systemClock.FastWorward(TimeSpan.FromDays(noOfDays-1));

            var returnRes = await _client.PostAsJsonAsync("/return-vehicle", new ReturnVehicleReq
            {
                BookingNumber = bookingNumber,
                MeterReadingKm = 200
            });

            var receipt = await returnRes.Content.ReadFromJsonAsync<VehicleReturnReceipt>();

            decimal priceKm = 2 * 1;
            decimal priceDay = 3 * 1.3m;


            Assert.Equivalent(new VehicleReturnReceipt
            {
                Cost = noOfDays * priceKm + priceDay * noOfDays ,
                DistanceTravelledKm = noOfKm,
                ElapsedDays = noOfDays,
            }, receipt);

        }





        public void SeedDb(CarRentalContext context)
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            context.BasePrices.Add(new BasePrice { IdBasePrice = 1, Amount = 2, PriceName = "BasePriceKm" });
            context.BasePrices.Add(new BasePrice { IdBasePrice = 2, Amount = 3, PriceName = "BasePriceDay" });
            context.SaveChanges();

            context.VehicleTypes.AddRange([
            new VehicleType {  IdVehicleType = 1,  KmFactor = 0m,  DayFactor = 1, Name = "Small car" },
            new VehicleType {  IdVehicleType = 2, KmFactor = 1m,  DayFactor = 1.3m, Name = "Combi" },
            new VehicleType {  IdVehicleType = 3, KmFactor = 1.5m,  DayFactor = 1.5m, Name = "Truck" },
            ]);

            context.SaveChanges();


            context.Vehicles.AddRange([new CarRentalDbModel.DbModels.Vehicle {
                        FkVehicleType = 1,
                        RegistrationNumber = "ABC123",
                    }
            ,new CarRentalDbModel.DbModels.Vehicle {
                        FkVehicleType = 2,
                        RegistrationNumber = "ABC124",
                    },
            new CarRentalDbModel.DbModels.Vehicle {
                        FkVehicleType = 3,
                        RegistrationNumber = "ABC125",
                    }]
                    );
            context.SaveChanges();

        }





    }
}