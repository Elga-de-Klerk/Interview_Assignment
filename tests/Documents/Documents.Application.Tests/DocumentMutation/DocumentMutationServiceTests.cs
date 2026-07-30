using Documents.Application.Abstractions;
using Documents.Application.DocumentMutation;
using NSubstitute;
using System.Text;

namespace Document.Application.Tests.DocumentMutation
{
    public class DocumentMutationServiceTests
    {
        [Fact]
        public async Task Mutate_WhenCalled_ShouldReturnNewDocumentMutationResult()
        {
            // Arrange
            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.CurrentDateTime.Returns(new DateTimeOffset(2026, 7, 30, 12, 0, 0, TimeSpan.Zero));

            var randomGenerator = Substitute.For<IRandomSequenceGenerator>();
            var randomSequence = "sdf3a21b";
            randomGenerator.Generate().Returns(randomSequence);

            var service = new DocumentMutationService(dateTimeProvider, randomGenerator);

            var fileName = "filename.txt";
            var content = "File content";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            var expectedContent =
                $"{content}{Environment.NewLine}{Environment.NewLine}" +
                $"Mutated on: 2026-07-30 12:00:00 +00:00 | Random sequence: {randomSequence}";

            // Act
            var result = await service.Mutate(fileName, stream);

            // Assert
            Assert.IsType<DocumentMutationResult>(result);
            Assert.Equal(fileName, result.FileName);
            
            var resultContentAsString = Encoding.UTF8.GetString(result.Content);
            Assert.Equal(expectedContent, resultContentAsString);
        }
    }
}
