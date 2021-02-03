using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingTripsCQRS.Events
{
    public class Event : INotification
    {
        public Guid Id;
        public DateTime CreatedAt;
        public Event()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
