using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DrivingTripsCQRS.Domain.DriverReport
{
    public interface IDriverReportRepository
    {
        IEnumerable<DriverReport> GetLatestReportOfAllDrivers();

        void AddDriverReport(DriverReport driverReport);
    }
}
