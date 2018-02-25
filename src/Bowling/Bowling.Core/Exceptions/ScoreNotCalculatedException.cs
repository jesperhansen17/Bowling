using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling.Core.Exceptions
{
    public class ScoreNotCalculatedException : Exception
    {
        public ScoreNotCalculatedException() : base() {}
        public ScoreNotCalculatedException(string message) : base(message) { }
        public ScoreNotCalculatedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
