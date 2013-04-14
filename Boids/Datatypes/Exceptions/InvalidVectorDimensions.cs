using System;

namespace Datatypes.Exceptions
{
    /// <summary>
    /// Simple exception with possibility for a string message.
    /// Name is self explanatory.
    /// </summary>
    public class InvalidVectorDimensions : System.Exception
    {
        /// <summary>
        /// Exception thrown when vector dimensions do not match.
        /// </summary>
        public InvalidVectorDimensions()
        {
        }

        /// <summary>
        /// Exception thrown when vector dimensions do not match.
        /// </summary>
        /// <param name="message">string to pass as a message for the exception.</param>
        public InvalidVectorDimensions(string message) : base(message)
        {
        }
    }
}
