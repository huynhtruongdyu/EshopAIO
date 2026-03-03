namespace EAIO.Shared.Domain.Abstraction
{
    /// <summary>
    /// Represents a domain error with a code and description.
    /// </summary>
    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "A null value was provided.");
        public static readonly Error NotFound = new("Error.NotFound", "The requested resource was not found.");
        public static readonly Error Validation = new("Error.Validation", "A validation error occurred.");
    }
}
