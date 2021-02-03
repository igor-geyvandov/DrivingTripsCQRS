using DrivingTripsCQRS.Domain.Driver;
using DrivingTripsCQRS.Domain.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingTripsCQRS.DataAccess
{
    public class InMemoryTripRepository : ITripRepository
    {
        private readonly List<Trip> _tripsStore = new List<Trip>();

        /// <summary>
        /// Gets all Trips matched by Driver name from the store.
        /// </summary>
        /// <param name="driverName"></param>
        /// <returns></returns>
        public IEnumerable<Trip> GetTripsByDriverName(string driverName)
        {
            return _tripsStore.Where(trip => trip.DriverName == driverName);
        }

        /// <summary>
        /// Adds Trip to the store.
        /// </summary>
        /// <param name="trip"></param>
        public void AddTrip(Trip trip)
        {
            _tripsStore.Add(trip);
        }
    }
}
