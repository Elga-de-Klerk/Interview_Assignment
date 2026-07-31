namespace Documents.Domain
{
    /// <summary>
    /// Class represents a simple text document.
    /// Encapsulates a method to mutate the file content.
    /// </summary>
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

        /// <summary>
        /// Method for mutating the TextDocument, 
        /// appending the given timestamp and random sequence.
        /// The original instance is left unchanged.
        /// </summary>
        /// <param name="timestamp">The current time in DateTimeOffset</param>
        /// <param name="randomSequence">A random sequence of characters</param>
        /// <returns cref="TextDocument"></returns>
        /// <exception cref="ArgumentException"></exception>
        public TextDocument Mutate(DateTimeOffset timestamp, string randomSequence)
        {
            if (string.IsNullOrWhiteSpace(randomSequence))
                throw new ArgumentException("Random sequence must be provided.", nameof(randomSequence));

            var mutatedContent =
                $"{Content}{Environment.NewLine}{Environment.NewLine}" +
                $"Mutated on: {timestamp:yyyy-MM-dd HH:mm:ss zzz} | Random sequence: {randomSequence}";

            return new TextDocument(FileName, mutatedContent);
        }
    }
}
