using System;

namespace CoreBox.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string errorMessage)
            : base(errorMessage)
        {
            
        }
    }
}