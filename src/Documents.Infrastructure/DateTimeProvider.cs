using Documents.Application.Abstractions;

namespace Documents.Infrastructure
{
    /// <inheritdoc/>
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset CurrentDateTime => DateTimeOffset.Now;
    }
}
