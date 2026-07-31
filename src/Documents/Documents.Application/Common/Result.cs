namespace Documents.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public T? Content { get; init; }
        public string? Error { get; init; }

        public static Result<T> Success(T content) => new() { IsSuccess = true, Content = content, Error = null };
        public static Result<T> Failure(string error) => new() { IsSuccess = false, Content = default, Error = error };
    }
}
