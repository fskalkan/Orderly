namespace Orderly.Application.Common.Exceptions;

public class AppValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public AppValidationException(IDictionary<string, string[]> errors)
        : base("Validation errors occurred.")
    {
        Errors = errors;
    }
}