namespace Documents.Application.Abstractions
{
    /// <summary>
    /// Service that holds all DateTime behavior.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Holds the current DateTimeOffset.
        /// </summary>
        DateTimeOffset CurrentDateTime { get; }
    }
}
