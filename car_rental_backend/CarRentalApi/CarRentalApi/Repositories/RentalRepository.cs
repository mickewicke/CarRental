using CarRentalApi.System;
using CarRentalDbModel.DbModels;
using Microsoft.EntityFrameworkCore;
namespace CarRentalApi.Repositories;

public interface IRentalRepository
{
    Task<IEnumerable<Rental>> GetActiveRentals();
    Task<IEnumerable<Vehicle>> GetAvailableVehicles();
    Task<int> GetOrCreateCustomer(string ssn);
    Task<Rental?> GetRentalByBookingNumber(string bookingNumber);
    Task<SystemBasePrice> GetSystemBasePrices();
    Task<VehicleType> GetVehicleTypeByCarID(int carID);
    Task RegisterVehiclePickup(PickupRegistration registration);
    Task RegisterVehicleReturn(ReturnRegistration registration);
}

public class RentalRepository(CarRentalContext context) : IRentalRepository
{
    public async Task<IEnumerable<Vehicle>> GetAvailableVehicles()
    {
        var q = from v in context.Vehicles.Include(v => v.FkVehicleTypeNavigation)
                join r in context.Rentals on v.IdVehicle equals r.FkVehicle into rv
                from r in rv.DefaultIfEmpty()
                where r == null
                select v;

        return (await q.ToListAsync()).Select(Vehicle.FromDbModel);
    }

    public async Task<VehicleType> GetVehicleTypeByCarID(int carID)
    {
        var vehicle = await context.Vehicles.Include(v => v.FkVehicleTypeNavigation).SingleAsync(v => v.IdVehicle == carID);
        return vehicle.FkVehicleTypeNavigation;
    }

    public async Task<SystemBasePrice> GetSystemBasePrices()
    {
        return new SystemBasePrice
        {
            BasePriceKm = await GetBasePriceById((int)BasePriceType.BasePriceKm),
            BasePriceDay = await GetBasePriceById((int)BasePriceType.BasePriceKm)
        };

    }

    public async Task<int> GetOrCreateCustomer(string ssn)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(c => c.Ssn == ssn);
        if (customer == null)
        {
            customer = new Customer
            {
                Ssn = ssn
            };
            await context.AddAsync(customer);
            await context.SaveChangesAsync();
        }
        return customer.IdCustomer;
    }


    private async Task<decimal> GetBasePriceById(int basepriceId)
    {
        return (await context.BasePrices.SingleAsync(bp => bp.IdBasePrice == basepriceId)).IdBasePrice;
    }


    public async Task RegisterVehiclePickup(PickupRegistration registration)
    {
        var vehicletype = await GetVehicleTypeByCarID(registration.CarID);

        var rental = new CarRentalDbModel.DbModels.Rental
        {
            FkVehicle = registration.CarID,
            FkCustomer = registration.CustomerID,
            BookingNumber = registration.BookingNumber,
            PickupMeeterKm = registration.PickupMeeterKm,
            PickupDateTime = registration.PickupDateTime,
            DayPrice = registration.DayPrice,
            KmPrice = registration.KmPrice
        };
        context.Rentals.Add(rental);
        await context.SaveChangesAsync();

        var res = context.Rentals.Include(r => r.FkVehicleNavigation.FkVehicleTypeNavigation).ToList();
    }

    public async Task RegisterVehicleReturn(ReturnRegistration registration)
    {
        var rental = await context.Rentals.SingleAsync(r => r.BookingNumber == registration.BookingNumber);
        rental.ReturnMeterKm = registration.ReturnMeterKm;
        rental.RentalPrice = registration.RentalPrice;
        rental.ReturnDateTime = registration.ReturnDateTime;
        await context.SaveChangesAsync();
    }


    public async Task<IEnumerable<Rental>> GetActiveRentals()
    {
        var r = await context.Rentals.Include(r => r.FkVehicleNavigation.FkVehicleTypeNavigation)
            .Where(r => r.ReturnDateTime == null)
            .ToListAsync();
        return r.Select(Rental.FromDb);
    }

    public async Task<Rental?> GetRentalByBookingNumber(string bookingNumber)
    {
        var rental = await context.Rentals.Include(r => r.FkVehicleNavigation.FkVehicleTypeNavigation).Include(r => r.FkCustomerNavigation)
            .Where(r => r.BookingNumber == bookingNumber)
            .SingleOrDefaultAsync();

        return Rental.FromDb(rental);
    }
}






