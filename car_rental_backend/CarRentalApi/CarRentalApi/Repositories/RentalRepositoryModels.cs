namespace CarRentalApi.Repositories;

public class PickupRegistration
{
    public int CarID { get; set; }
    public int CustomerID { get; set; }
    public string BookingNumber { get; set; }
    public int PickupMeeterKm { get; set; }
    public DateTime PickupDateTime { get; set; }
    public decimal DayPrice { get; set; }
    public decimal KmPrice { get; set; }
}

public class ReturnRegistration
{
    public string BookingNumber { get; set; }
    public int ReturnMeterKm { get; set; }
    public decimal RentalPrice { get; set; }
    public DateTime ReturnDateTime { get; set; }
}


public class SystemBasePrice
{
    public decimal BasePriceKm { get; set; }
    public decimal BasePriceDay { get; set; }
}

public class Rental
{
    public static Rental? FromDb(CarRentalDbModel.DbModels.Rental? rental)
    {
        if (rental == null)
        {
            return null;
        }
        return new Rental
        {
            BookingNumber = rental.BookingNumber,
            CustomerSSN = rental.FkCustomerNavigation.Ssn,
            PickupMeeterKm = rental.PickupMeeterKm,
            PickupDateTime = rental.PickupDateTime,
            DayPrice = rental.DayPrice,
            KmPrice = rental.KmPrice,
            Vehicle = Vehicle.FromDbModel(rental.FkVehicleNavigation)
        };
    }
    public string CustomerSSN { get; set; }
    public string BookingNumber { get; set; }
    public decimal DayPrice { get; set; }
    public decimal KmPrice { get; set; }
    public int PickupMeeterKm { get; set; }
    public DateTime PickupDateTime { get; set; }
    public Vehicle Vehicle { get; set; }

}

public class Vehicle
{
    public static Vehicle FromDbModel(CarRentalDbModel.DbModels.Vehicle vehicle)
    {
        return new Vehicle
        {
            VehicleID = vehicle.IdVehicle,
            RegistrationNumber = vehicle.RegistrationNumber,
            TypeName = vehicle.FkVehicleTypeNavigation?.Name
        };
    }

    public int VehicleID { get; set; }
    public string RegistrationNumber { get; set; }
    public string TypeName { get; set; }
}
