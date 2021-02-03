using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingTripsCQRS.Exceptions
{
    [Serializable]
    public class MissingCommandsException : Exception
    {
        public MissingCommandsException()
        {
        }

        public MissingCommandsException(string commandFilePath)
            : base(String.Format("No valid Driver or Trip commands found in file {0}", commandFilePath))
        {
        }
    }
}
