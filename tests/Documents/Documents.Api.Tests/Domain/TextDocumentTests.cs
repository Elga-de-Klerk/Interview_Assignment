using Documents.Domain;

namespace Documents.Tests.Domain
{
    public class TextDocumentTests
    {
        [Fact]
        public void TextDocument_WhenAllArgumentsAreProvided_ShouldCreateTextDocument()
        {
            // Arrange
            var FileName = "filename.txt";
            var Content = "File content";

            // Act
            var textDocument = new TextDocument(FileName, Content);

            // Assert
            Assert.Equal(FileName, textDocument.FileName);
            Assert.Equal(Content, textDocument.Content);
        }

        [Fact]
        public void TextDocument_WhenContentIsNull_ShouldCreateTextDocumentWithEmptyContent()
        {
            // Arrange
            var FileName = "filename.txt";

            // Act
            var textDocument = new TextDocument(FileName, null!);

            // Assert
            Assert.Equal(FileName, textDocument.FileName);
            Assert.Equal(string.Empty, textDocument.Content);
        }

        [Fact]
        public void TextDocument_WhenFileNameIsMissing_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new TextDocument("", "File content"));
        }

        [Fact]
        public void Mutate_WhenAllArgumentsAreProvided_ShouldMutateTheContentAndCreateTextDocument()
        {
            // Arrange
            var FileName = "filename.txt";
            var Content = "File content";
            var textDocument = new TextDocument(FileName, Content);

            var timestamp = new DateTimeOffset(2026, 7, 30, 12, 0, 0, TimeSpan.Zero);
            var randomSequence = "sdf3a21b";

            var expectedContent =
                $"{Content}{Environment.NewLine}{Environment.NewLine}" +
                $"Mutated on: 2026-07-30 12:00:00 +00:00 | Random sequence: {randomSequence}";

            // Act
            var mutatedDocument = textDocument.Mutate(timestamp, randomSequence);

            // Assert
            Assert.IsType<TextDocument>(mutatedDocument);
            Assert.Equal(FileName, mutatedDocument.FileName);
            Assert.Equal(expectedContent, mutatedDocument.Content);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Mutate_WhenRandomSequenceIsMissing_ShouldThrowArgumentException(string? randomSequence)
        {
            // Arrange
            var textDocument = new TextDocument("filename.txt", "File content");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => textDocument.Mutate(DateTimeOffset.UtcNow, randomSequence!));
        }
    }
}
