using System;

namespace Datatypes.Exceptions
{
    // Simple exception with message and innerexception. Name us self explanatory.
    class InvalidVectorDimensions : System.Exception
    {
        public InvalidVectorDimensions()
        {
        }

        public InvalidVectorDimensions(string message) : base(message)
        {
        }

        public InvalidVectorDimensions(string message, Exception innerExeption)
            : base(message, innerExeption)
        {
        }
    }
}
