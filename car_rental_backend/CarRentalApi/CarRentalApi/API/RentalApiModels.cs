namespace CarRentalApi.API;

public class PickupVehicleReq
{
    public string BookingNumber { get; set; }
    public int CarID { get; set; }
    public string CustomerSSN { get; set; }
    public int MeterReadingKm { get; set; }
}

public class ReturnVehicleReq
{
    public string BookingNumber { get; set; }
    public int MeterReadingKm { get; set; }
    public string CustomerSSN { get; set; }
}


public record CreateCustomerReq(string ssn);