using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DrivingTripsCQRS.Domain.Driver
{
    public interface IDriverRepository
    {
        Driver GetDriverByName(string name);

        void AddDriver(Driver driver);
    }
}
