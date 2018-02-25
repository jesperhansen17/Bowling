using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling.Game.Exceptions
{
    public class InvalidScoreException : Exception
    {
        public InvalidScoreException() : base() { }

        public InvalidScoreException(string message) : base(message) { }

        public InvalidScoreException(string message, Exception innerException) : base(message, innerException) { }
    }
}
