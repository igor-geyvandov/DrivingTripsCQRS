using MediatR;
using DrivingTripsCQRS.Domain.DriverReport;
using DrivingTripsCQRS.Domain.Trip;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DrivingTripsCQRS.Events.Handlers
{
    /// <summary>
    /// Handles TripAddedEvent by calculating the latest DriverReport of all trip for the given Driver and 
    /// then saving it to the store. 
    /// </summary>
    public class TripAddedEventHandler : INotificationHandler<TripAddedEvent>
    {
        private readonly IDriverReportRepository _driverReportRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IEventRepository _eventRepository;

        public TripAddedEventHandler(IDriverReportRepository driverReportRepository, ITripRepository tripRepository, IEventRepository eventRepository)
        {
            _driverReportRepository = driverReportRepository ?? throw new ArgumentNullException(nameof(driverReportRepository));
            _tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public Task Handle(TripAddedEvent @event, CancellationToken cancellationToken)
        {
            _eventRepository.AddEvent(@event);
            var trips = _tripRepository.GetTripsByDriverName(@event.DriverName);
            if (trips.Any())
            {
                var totalMiles = trips.Sum(trip => trip.MilesDriven);
                var totalTimeInHours = trips.Sum(trip => (trip.StopTime - trip.StartTime).TotalHours);
                var averageSpeed = totalMiles > 0 && totalTimeInHours > 0 ? totalMiles / totalTimeInHours : 0.0;
                var driverReport = new DriverReport
                {
                    Name = @event.DriverName,
                    TotalMiles = (int)Math.Round(totalMiles),
                    AverageSpeed = (int)Math.Round(totalMiles / totalTimeInHours)
                };
                _driverReportRepository.AddDriverReport(driverReport);
            }            
            return Task.CompletedTask;
        }
    }
}
