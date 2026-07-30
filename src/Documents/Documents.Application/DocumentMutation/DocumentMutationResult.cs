namespace Documents.Application.DocumentMutation
{
    public sealed record DocumentMutationResult
    {
        public required string FileName { get; init; }

        public required byte[] Content { get; init; }
    }
}
