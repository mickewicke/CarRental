namespace CarRentalApi.Services.Price;

static class RentalCalculationExtensions
{
    public static decimal CostCalculation(this CostBasis basis)
    {
        return basis.TimeCost() + basis.DistanceCost();
    }

    public static VehicleReturnReceipt Receipt(this CostBasis basis)
    {
        return new VehicleReturnReceipt
        {
            DistanceTravelledKm = basis.DistanceTravelledKm(),
            ElapsedDays = basis.DaysElapsed(),
            Cost = basis.CostCalculation()
        };
    }

    public static decimal TimeCost(this CostBasis basis)
    {
        var numberOfBegunDays = basis.DaysElapsed();
        var timeCost = numberOfBegunDays * basis.DayPrice;
        return timeCost;
    }

    public static int DaysElapsed(this CostBasis basis)
    {
        return (basis.EndDate - basis.StartDate).Days + 1;
    }


    public static decimal DistanceCost(this CostBasis basis)
    {
        var numberOfKm = basis.MeterEnd - basis.MeterStart;
        var distanceCost = numberOfKm * basis.KmPrice;
        return distanceCost;
    }

    public static int DistanceTravelledKm(this CostBasis basis)
    {
        return basis.MeterEnd - basis.MeterStart;

    }
}

