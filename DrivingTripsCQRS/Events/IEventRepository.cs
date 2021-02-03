using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DrivingTripsCQRS.Events
{
    public interface IEventRepository
    {
        void AddEvent(Event driver);
    }
}
