using CarRentalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalTests.MockClasses
{
    public class MockSystemClock : ISystemClock
    {
        public DateTime Now => _now;  
        
        
        private DateTime _now  = DateTime.Now;

        public void FastWorward(TimeSpan timeIncrement)
        {
            _now = _now.Add(timeIncrement);
        }

        public void SetSystemTime (DateTime dateTime)
        {
            _now = dateTime;
        }

    }
}
