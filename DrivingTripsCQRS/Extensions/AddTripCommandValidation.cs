using DrivingTripsCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingTripsCQRS.Extensions
{
    public static class AddTripCommandValidation
    {
        /// <summary>
        /// Returns TRUE if AddTripCommand is valid and average speed is between 5 mph and 100 mph. FALSE otherwise.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static bool IsValid(this AddTripCommand cmd)
        {
            if (!string.IsNullOrEmpty(cmd.DriverName) && cmd.MilesDriven > 0 && cmd.StopTime > cmd.StartTime)
            {
                var speed = cmd.MilesDriven / (cmd.StopTime - cmd.StartTime).TotalHours;
                if (speed >= 5 && speed <= 100)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
