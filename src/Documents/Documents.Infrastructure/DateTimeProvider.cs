using Documents.Application.Abstractions;

namespace Documents.Infrastructure
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset CurrentDateTime => DateTimeOffset.Now;
    }
}
