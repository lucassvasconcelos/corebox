namespace CoreBox.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string errorMessage)
        : base(errorMessage)
    {

    }
}