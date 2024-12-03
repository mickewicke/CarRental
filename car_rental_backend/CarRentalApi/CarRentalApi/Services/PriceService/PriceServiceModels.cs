namespace CarRentalApi.Services.Price;
public class VehicleReturnReceipt
{
    public int DistanceTravelledKm
    {
        get; set;
    }

    public int ElapsedDays { get; set; }

    public decimal Cost
    {
        get; set;

    }
}


public class CostBasis
{
    public int MeterStart { get; set; }
    public int MeterEnd { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal DayPrice { get; set; }
    public decimal KmPrice { get; set; }
}


public class VehiclePrice
{
    public decimal PricePerDay { get; set; }
    public decimal PricePerKm { get; set; }
}