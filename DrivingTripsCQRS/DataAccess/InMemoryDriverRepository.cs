using DrivingTripsCQRS.Domain.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingTripsCQRS.DataAccess
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private readonly List<Driver> _driversStore = new List<Driver>();

        /// <summary>
        /// Gets Driver matched by Name from the store.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Driver GetDriverByName(string name)
        {
            var drivers = _driversStore.Where(driver => driver.Name == name);
            return drivers.Any() ? drivers.First() : null;
        }

        /// <summary>
        /// Adds Driver to the store.
        /// </summary>
        /// <param name="driver"></param>
        public void AddDriver(Driver driver)
        {
            _driversStore.Add(driver);
        }
    }
}
