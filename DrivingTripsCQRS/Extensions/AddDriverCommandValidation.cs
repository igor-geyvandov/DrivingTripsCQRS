using DrivingTripsCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingTripsCQRS.Extensions
{
    /// <summary>
    /// Returns TRUE when AddDriverCommand is valid. 
    /// </summary>
    public static class AddDriverCommandValidation
    {
        public static bool IsValid(this AddDriverCommand cmd)
        {
            return !string.IsNullOrEmpty(cmd.Name);
        }
    }
}
