using MediatR;
using DrivingTripsCQRS.Domain.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using DrivingTripsCQRS.Extensions;
using DrivingTripsCQRS.Events;

namespace DrivingTripsCQRS.Commands.Handlers
{
    /// <summary>
    /// Handles AddDriverCommand by saving Driver to the storage repo and then publishing a DriverAddEvent.
    /// </summary>
    public class AddDriverCommandHandler : IRequestHandler<AddDriverCommand, bool>
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMediator _mediator;

        public AddDriverCommandHandler(IDriverRepository driverRepository, IMediator mediator)
        {
            _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<bool> Handle(AddDriverCommand cmd, CancellationToken cancellationToken)
        {
            var driverAdded = false;
            if (cmd.IsValid())
            {
                var driver = new Driver { Name = cmd.Name };
                if (_driverRepository.GetDriverByName(driver.Name) == null)
                {
                    _driverRepository.AddDriver(driver);
                    driverAdded = true;
                    _mediator.Publish(new DriverAddedEvent { DriverName = cmd.Name });
                }                
            }            
            return Task.FromResult(driverAdded);
        }
    }
}
