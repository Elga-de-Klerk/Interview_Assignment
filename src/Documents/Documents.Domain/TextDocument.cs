namespace Documents.Domain
{
    public sealed record TextDocument
    {
        public string FileName { get; }
        public string Content { get; }

        public TextDocument(string fileName, string content)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name must be provided.", nameof(fileName));

            FileName = fileName;
            Content = content ?? string.Empty;
        }

        public TextDocument Mutate(DateTimeOffset timestamp, string randomSequence)
        {
            if (string.IsNullOrEmpty(randomSequence))
                throw new ArgumentException("Random sequence must be provided.", nameof(randomSequence));

            var mutatedContent =
                $"{Content}{Environment.NewLine}{Environment.NewLine}" +
                $"Mutated on: {timestamp:yyyy-MM-dd HH:mm:ss zzz} | Random sequence: {randomSequence}";

            return new TextDocument(FileName, mutatedContent);
        }
    }
}
