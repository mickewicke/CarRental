using CarRentalApi.Models;

namespace CarRentalApi.Services
{
    public class SystemClock : ISystemClock
    {
        public DateTime Now => DateTime.Now;
    }
}
