using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingTripsCQRS.Domain.Trip
{
    public class Trip
    {
        public string DriverName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan StopTime { get; set; }
        public double MilesDriven { get; set; }
    }
}
