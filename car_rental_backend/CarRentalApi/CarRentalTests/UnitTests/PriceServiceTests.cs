using CarRentalApi.Repositories;
using CarRentalApi.Services.Price;
using CarRentalApi.System;
using CarRentalDbModel.DbModels;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalTests.UnitTests
{

    public class PriceServiceTests
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly PriceService _priceService;
        public PriceServiceTests()
        {
            _rentalRepository = Substitute.For<IRentalRepository>();
            _priceService = new PriceService(_rentalRepository);
        }


        //Hämta och lämna samma kalenderdag räknas som 1 dag
        //Lämna nästa kalenderdag räknas som 2 dagar    
        [Theory]
        [InlineData("2024-12-01 12:00", "2024-12-01 12:00", 1)]
        [InlineData("2024-12-01 23:59", "2024-12-02 00:00", 2)]
        [InlineData("2024-12-01 00:00", "2024-12-02 23:59", 2)]
        [InlineData("2024-12-01 12:00", "2024-12-03 12:00", 3)]
        public async Task TestDayPrice(DateTime start, DateTime end, int expectedDayCount)
        {

            var costBasis = new CostBasis
            {
                MeterStart = 100,
                MeterEnd = 200,
                StartDate = start,
                EndDate = end,
                DayPrice = 100,
                KmPrice = 1
            };

            var receipt = await _priceService.GetReturnReceipt(costBasis);


            Assert.Equal(receipt.ElapsedDays, receipt.ElapsedDays);

        }

        [Theory]
        [InlineData(100, 100, 0)]
        [InlineData(100, 200, 100)]
        public async Task TestDistance(int meeterSart, int meeterEnd, int expectedDistance)
        {
            var costBasis = new CostBasis
            {
                DayPrice = 100,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                KmPrice = 1,
                MeterEnd = meeterEnd,
                MeterStart = meeterSart
            };

            var reciept = await _priceService.GetReturnReceipt(costBasis);

            Assert.Equal(expectedDistance, reciept.DistanceTravelledKm);

        }


        [Theory]
        [InlineData(0, 5)]
        [InlineData(5, 0)]
        [InlineData(0, 0)]
        [InlineData(5, 7)]
        public async Task TestPrice(int dayPrice, int kmPrice)
        {
            var noOfDays = 2;
            var noOfKm = 3;

            var costBasis = new CostBasis
            {
                DayPrice = dayPrice,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(noOfDays-1),
                KmPrice = kmPrice,
                MeterStart = 100 - noOfKm,
                MeterEnd = 100
            };

            var reciept = await _priceService.GetReturnReceipt(costBasis);

            Assert.Equal(noOfDays * dayPrice + noOfKm * kmPrice, reciept.Cost);

        }

        [Fact]        
        public async Task TestGetCarPrice()
        {
            var id = 1;

            _rentalRepository.GetSystemBasePrices().Returns(new SystemBasePrice
            {
                BasePriceDay = 2,
                BasePriceKm = 3
            });

            _rentalRepository.GetVehicleTypeByCarID(id).Returns(new VehicleType
            {
                DayFactor = 5,
                KmFactor =7,
            });


            var price = await _priceService.GetCarPrice(id);


            Assert.Equal(2 * 5, price.PricePerDay);
            Assert.Equal(3 * 7, price.PricePerKm);

        }


        

    }
}
