namespace CoreBox.Exceptions;

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string errorMessage)
        : base(errorMessage)
    {

    }
}