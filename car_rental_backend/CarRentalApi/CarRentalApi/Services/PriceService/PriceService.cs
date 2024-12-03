using CarRentalApi.Repositories;
using CarRentalDbModel.DbModels;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Threading;

namespace CarRentalApi.Services.Price;

public class PriceService(IRentalRepository repository)
{

    public async Task<VehicleReturnReceipt> GetReturnReceipt(CostBasis costBasis)
    {
        return costBasis.Receipt();
    }

    public async Task<VehiclePrice> GetCarPrice(int carID)
    {
        var systemPrice = await repository.GetSystemBasePrices();
        var vehicleType = await repository.GetVehicleTypeByCarID(carID);

        return new VehiclePrice
        {
            PricePerDay = systemPrice.BasePriceDay * vehicleType.DayFactor,
            PricePerKm = systemPrice.BasePriceKm * vehicleType.KmFactor
        };
    }
}
