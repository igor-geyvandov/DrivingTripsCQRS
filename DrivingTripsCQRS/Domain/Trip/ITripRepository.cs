using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DrivingTripsCQRS.Domain.Trip
{
    public interface ITripRepository
    {
        IEnumerable<Trip> GetTripsByDriverName(string driverName);

        void AddTrip(Trip trip);
    }
}
