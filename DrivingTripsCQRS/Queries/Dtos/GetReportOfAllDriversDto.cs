using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingTripsCQRS.Queries.Dtos
{
    public class GetReportOfAllDriversDto
    {
        public IEnumerable<DriverReportDto> DriverReports { get; set; }
    }
}
