using DrivingTripsCQRS.Domain.Driver;
using DrivingTripsCQRS.Domain.Trip;
using DrivingTripsCQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingTripsCQRS.DataAccess
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _eventStore = new List<Event>();

        /// <summary>
        /// Adds Event to the store.
        /// </summary>
        /// <param name="trip"></param>
        public void AddEvent(Event trip)
        {
            _eventStore.Add(trip);
        }
    }
}
