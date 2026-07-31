using Documents.Application.Abstractions;
using Documents.Application.DocumentMutation;
using Documents.Tests.Application.Fixtures;
using NSubstitute;
using System.Text;

namespace Documents.Tests.Application.DocumentMutation
{
    public class DocumentMutationServiceTests
    {
        [Fact]
        public async Task Mutate_WhenInputIsValid_ShouldReturnNewDocumentMutationResult()
        {
            // Arrange
            var fixture = new DocumentMutationServiceFixture();
            var dateTimeOffset = new DateTimeOffset(2026, 7, 30, 12, 0, 0, TimeSpan.Zero);
            var randomSequence = "sdf3a21b";

            var service = fixture
                .SetupDateTimeProviderToReturn(dateTimeOffset)
                .SetupRandomSequenceGeneratorToReturn(randomSequence)
                .Build();

            var fileName = "filename.txt";
            var content = "File content";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            var expectedContent =
                $"{content}{Environment.NewLine}{Environment.NewLine}" +
                $"Mutated on: 2026-07-30 12:00:00 +00:00 | Random sequence: {randomSequence}";

            // Act
            var result = await service.Mutate(fileName, stream, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            var documentMutationResult = Assert.IsType<DocumentMutationResult>(result.Content);
            Assert.Equal(fileName, documentMutationResult.FileName);
            
            var resultContentAsString = Encoding.UTF8.GetString(documentMutationResult.Content);
            Assert.Equal(expectedContent, resultContentAsString);
        }

        [Fact]
        public async Task Mutate_WhenCancellationIsRequested_ShouldThrowAnOperationCanceledException()
        {
            // Arrange
            var fixture = new DocumentMutationServiceFixture();
            var service = fixture.Build();

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("File content"));

            var cancelationTokenSource = new CancellationTokenSource();
            cancelationTokenSource.Cancel();

            // Act & Assert
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                service.Mutate("filename.txt", stream, cancelationTokenSource.Token));
        }

        public static IEnumerable<object[]> InvalidUtf8ByteSequences()
        {
            yield return [new byte[] { 0xFF, 0xFE }];
            yield return [new byte[] { 0xC0, 0x80 }];
            yield return [new byte[] { 0x80, 0x80, 0x80 }]; 
        }

        [Theory]
        [MemberData(nameof(InvalidUtf8ByteSequences))]
        public async Task Mutate_WhenFileHasInvalidTextContent_ShouldReturnAnError(byte[] invalidBytes)
        {
            // Arrange
            var fixture = new DocumentMutationServiceFixture();
            var service = fixture.Build();

            using var stream = new MemoryStream(invalidBytes);

            // Act
            var result = await service.Mutate("image.png", stream, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The uploaded file does not contain valid text content.", result.Error);
        }
    }
}
