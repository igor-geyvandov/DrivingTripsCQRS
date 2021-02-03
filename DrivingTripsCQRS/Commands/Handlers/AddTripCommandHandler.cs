using MediatR;
using DrivingTripsCQRS.Domain.Trip;
using System;
using System.Threading;
using System.Threading.Tasks;
using DrivingTripsCQRS.Extensions;
using DrivingTripsCQRS.Events;

namespace DrivingTripsCQRS.Commands.Handlers
{
    /// <summary>
    /// Handles AddTripCommand by saving Trip to the storage repo and then publishing a TripAddedEvent.
    /// </summary>
    public class AddTripCommandHandler : IRequestHandler<AddTripCommand, bool>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IMediator _mediator;
        public AddTripCommandHandler(ITripRepository tripRepository, IMediator mediator)
        {
            _tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<bool> Handle(AddTripCommand cmd, CancellationToken cancellationToken)
        {
            var tripAdded = false;
            if (cmd.IsValid())
            {
                var trip = new Trip {
                    DriverName = cmd.DriverName,
                    MilesDriven = cmd.MilesDriven,
                    StartTime = cmd.StartTime,
                    StopTime = cmd.StopTime,
                };
                _tripRepository.AddTrip(trip);
                tripAdded = true;
                _mediator.Publish(new TripAddedEvent { DriverName = cmd.DriverName });
            }           
            return Task.FromResult(tripAdded);
        }
    }
}
