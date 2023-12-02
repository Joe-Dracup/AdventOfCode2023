using System;

public class DayDoesNotExistException : Exception
{
    public DayDoesNotExistException()
    {
    }

    public DayDoesNotExistException(string message)
        : base(message)
    {
    }

    public DayDoesNotExistException(string message, Exception inner)
        : base(message, inner)
    {
    }
}