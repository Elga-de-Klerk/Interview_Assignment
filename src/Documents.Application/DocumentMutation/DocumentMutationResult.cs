namespace Documents.Application.DocumentMutation
{
    /// <summary>
    /// Record represents the mutated text file result.
    /// </summary>
    public sealed record DocumentMutationResult
    {
        public required string FileName { get; init; }

        public required byte[] Content { get; init; }
    }
}
