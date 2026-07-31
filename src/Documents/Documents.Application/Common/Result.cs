namespace Documents.Application.Common
{
    /// <summary>
    /// Represents the outcome of an operation that can either succeed with a
    /// content of type <typeparamref name="T"/>, or fail with a descriptive error
    /// message.
    /// </summary>
    /// <typeparam name="T">The type of content produced when the operation succeeds.</typeparam>
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public T? Content { get; init; }
        public string? Error { get; init; }

        /// <summary>
        /// Creates a successful result containing the given content.
        /// </summary>
        /// <param name="content">The content produced by a successful operation.</param>
        /// <returns cref="Result<T>">A successful result.</returns>
        public static Result<T> Success(T content) => new() { IsSuccess = true, Content = content, Error = null };
        /// <summary>
        /// Creates a failed result containing the given error messsage.
        /// </summary>
        /// <param name="error">A description of why the operion failed.</param>
        /// <returns cref="Result<T>">A failed result.</returns>
        public static Result<T> Failure(string error) => new() { IsSuccess = false, Content = default, Error = error };
    }
}
