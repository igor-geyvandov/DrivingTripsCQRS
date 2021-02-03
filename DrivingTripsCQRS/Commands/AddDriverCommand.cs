using System;
using MediatR;

namespace DrivingTripsCQRS.Commands
{
    /// <summary>
    /// Command to add Driver.
    /// </summary>
    public class AddDriverCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }
}
