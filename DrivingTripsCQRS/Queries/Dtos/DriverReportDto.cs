using MediatR;
using DrivingTripsCQRS.Domain.Driver;
using DrivingTripsCQRS.Domain.DriverReport;
using System.Collections.Generic;

namespace DrivingTripsCQRS.Queries.Dtos
{
    public class DriverReportDto
    {
        public string Name { get; set; }
        public int TotalMiles { get; set; }
        public int AverageSpeed { get; set; }       
    }
}
