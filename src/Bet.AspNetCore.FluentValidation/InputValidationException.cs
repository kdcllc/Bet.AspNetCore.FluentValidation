namespace Bet.AspNetCore.FluentValidation
{
    public class InputValidationException : Exception
    {
        public InputValidationException() : base()
        {
        }

        public InputValidationException(string? message) : base(message)
        {
        }

        public InputValidationException(string? message, IDictionary<string, string[]> errors) : base(message)
        {
            Errors = errors;
        }

        public InputValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public IDictionary<string, string[]> Errors { get; set; }
    }
}
