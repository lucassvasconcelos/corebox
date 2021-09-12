using System;

namespace CoreBox.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}