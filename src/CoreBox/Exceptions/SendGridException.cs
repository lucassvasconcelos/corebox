namespace CoreBox.Exceptions;

public class SendGridException : Exception
{
    public SendGridException(string errorMessage)
        : base(errorMessage)
    {

    }
}