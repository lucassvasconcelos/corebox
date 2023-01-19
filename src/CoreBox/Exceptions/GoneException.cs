namespace CoreBox.Exceptions;

public class GoneException : Exception
{
    public GoneException(string errorMessage)
        : base(errorMessage)
    {

    }
}