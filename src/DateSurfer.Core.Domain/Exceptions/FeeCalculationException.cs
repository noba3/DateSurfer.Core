namespace DateSurfer.Core.Domain.Exceptions;

public class FeeCalculationException : Exception
{
    public FeeCalculationException(string message) : base(message) { }
    public FeeCalculationException(string message, Exception inner) : base(message, inner) { }
}