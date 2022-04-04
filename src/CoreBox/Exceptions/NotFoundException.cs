namespace CoreBox.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string errorMessage)
        : base(errorMessage)
    {

    }
}