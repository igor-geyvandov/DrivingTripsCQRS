using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingTripsCQRS.Events
{
    public class DriverAddedEvent : Event
    {
        public string DriverName { get; set; }
    }
}
