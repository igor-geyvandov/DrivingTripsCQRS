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
    /// Handles DriverAddedEvent by adding a "tripless" DriverReport to the store. 
    /// </summary>
    public class DriverAddedEventHandler : INotificationHandler<DriverAddedEvent>
    {
        private readonly IDriverReportRepository _driverReportRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IEventRepository _eventRepository;

        public DriverAddedEventHandler(IDriverReportRepository driverReportRepository, ITripRepository tripRepository, IEventRepository eventRepository)
        {
            _driverReportRepository = driverReportRepository ?? throw new ArgumentNullException(nameof(driverReportRepository));
            _tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public Task Handle(DriverAddedEvent @event, CancellationToken cancellationToken)
        {
            _eventRepository.AddEvent(@event);
            var trips = _tripRepository.GetTripsByDriverName(@event.DriverName);
            if (!trips.Any())
            {
                _driverReportRepository.AddDriverReport(new DriverReport
                {
                    Name = @event.DriverName,
                    TotalMiles = 0,
                    AverageSpeed = 0
                });
            }            
            return Task.CompletedTask;
        }
    }
}
