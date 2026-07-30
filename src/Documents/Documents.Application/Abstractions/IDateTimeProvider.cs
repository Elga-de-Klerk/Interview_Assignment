namespace Documents.Application.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTimeOffset CurrentDateTime { get; }
    }
}
