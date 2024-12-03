using CarRentalApi.API;
using CarRentalApi.Models;
using CarRentalApi.Repositories;
using CarRentalApi.Services.Price;
using CarRentalApi.System;

namespace CarRentalApi.Services.RentalService;

public class RentalService(IRentalRepository dbServices, ISystemClock systemClock, PriceService priceService)
{

    public async Task RegisterVehiclePickup(PickupVehicleReq req)
    {
        var customerID = await dbServices.GetOrCreateCustomer(req.CustomerSSN);
        var vehicletype = await dbServices.GetVehicleTypeByCarID(req.CarID);
        var vehiclePrice = await priceService.GetCarPrice(vehicletype.IdVehicleType);

        await dbServices.RegisterVehiclePickup(new PickupRegistration
        {
            CarID = req.CarID,
            CustomerID = customerID,
            BookingNumber = req.BookingNumber,
            PickupMeeterKm = req.MeterReadingKm,
            PickupDateTime = systemClock.Now,
            DayPrice = vehiclePrice.PricePerDay,
            KmPrice = vehiclePrice.PricePerKm
        });
    }

    public async Task<IEnumerable<Rental>> GetActiveRentals()
    {
        return await dbServices.GetActiveRentals();
    }

    public async Task<VehicleReturnReceipt> RegisterVehicleReturn(ReturnVehicleReq req)
    {
        var activeRental = await dbServices.GetRentalByBookingNumber(req.BookingNumber);
        if (activeRental == null)
        {
            throw new InvalidOperationException("Booking number not found");
        }

        var receipt = await GetReturnReceipt(req, activeRental);

        await dbServices.RegisterVehicleReturn(new ReturnRegistration
        {
            BookingNumber = req.BookingNumber,
            ReturnMeterKm = req.MeterReadingKm,
            ReturnDateTime = systemClock.Now,
            RentalPrice = receipt.Cost
        });

        return receipt;
    }

    public async Task<IEnumerable<Vehicle>> GetAvailabelVehicles()
    {
        return await dbServices.GetAvailableVehicles();
    }


    private async Task<VehicleReturnReceipt> GetReturnReceipt(ReturnVehicleReq req, Rental activeRental)
    {
        return await priceService.GetReturnReceipt(new CostBasis
        {
            DayPrice = activeRental.DayPrice,
            KmPrice = activeRental.KmPrice,

            MeterStart = activeRental.PickupMeeterKm,
            StartDate = activeRental.PickupDateTime,

            MeterEnd = req.MeterReadingKm,
            EndDate = systemClock.Now,

        });
    }




}


