using DrivingTripsCQRS.Domain.DriverReport;
using DrivingTripsCQRS.Domain.Trip;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrivingTripsCQRS.DataAccess
{
    public class InMemoryDriverReportRepository : IDriverReportRepository
    {
        private readonly List<DriverReport> _driverReportStore = new List<DriverReport>();

        /// <summary>
        /// Gets the latest DriverReport for each Driver from the store.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DriverReport> GetLatestReportOfAllDrivers()
        {
            //Groups all DriverReport object by Name and take the latest DriverReport for each Driver
            var groupsOfReports = _driverReportStore.GroupBy(dr => dr.Name);
            var reports = groupsOfReports.Select(group =>
            {
                //Returns the DriverReport with the latest CreatedAt timestamp.
                return group.OrderByDescending(report => report.CreatedAt).First();
            });
            return reports;
        }

        /// <summary>
        /// Adds DriverReport to the store.
        /// </summary>
        /// <param name="driverReport"></param>
        public void AddDriverReport(DriverReport driverReport)
        {
            driverReport.CreatedAt = DateTime.Now;
            _driverReportStore.Add(driverReport);
        }
    }
}
