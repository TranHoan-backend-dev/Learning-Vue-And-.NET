namespace Model.Exception;

public class ExistingException: System.Exception
{
    public ExistingException() : base("Duplicated resources.")
    {
    }

    public ExistingException(string message) : base(message)
    {
    }

    public ExistingException(string message, System.Exception? innerException) : base(message, innerException)
    {
    }
}