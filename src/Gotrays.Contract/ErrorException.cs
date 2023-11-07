namespace Gotrays.Contract;

public class ErrorException : Exception
{
    public ErrorException(string message):base(message)
    {
    }
}