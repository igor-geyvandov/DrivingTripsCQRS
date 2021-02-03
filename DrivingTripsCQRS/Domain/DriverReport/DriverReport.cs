using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingTripsCQRS.Domain.DriverReport
{
    public class DriverReport
    {
        public string Name { get; set; }
        public int TotalMiles { get; set; }
        public int AverageSpeed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
