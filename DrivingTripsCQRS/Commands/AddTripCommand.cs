using System;
using MediatR;

namespace DrivingTripsCQRS.Commands
{
    /// <summary>
    /// Command to add Trip.
    /// </summary>
    public class AddTripCommand : IRequest<bool>
    {
        public string DriverName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan StopTime { get; set; }
        public double MilesDriven { get; set; }
    }
}
