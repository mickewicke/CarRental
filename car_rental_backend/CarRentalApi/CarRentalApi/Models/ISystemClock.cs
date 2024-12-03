namespace CarRentalApi.Models
{
    public interface ISystemClock
    {
        public DateTime Now { get; }
    }
}
