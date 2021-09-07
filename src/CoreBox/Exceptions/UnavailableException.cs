using System;

namespace CoreBox.Exceptions
{
    public class UnavailableException : Exception
    {
        public UnavailableException(string errorMessage)
            : base(errorMessage)
        {
            
        }
    }
}