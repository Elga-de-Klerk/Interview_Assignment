using Documents.Infrastructure;

namespace Documents.Tests.Infrastructure
{
    public class RandomSequenceGeneratorTests
    {
        [Fact]
        public void Generate_WhenNoLengthDefined_ShouldReturnDefault8CharacterRandomSequence()
        {
            // Arrange
            var generator = new RandomSequenceGenerator();

            // Act
            var result = generator.Generate();

            // Assert
            Assert.Equal(8, result.Length);
            Assert.Matches("^[A-Za-z0-9]+$", result);
        }

        [Fact]
        public void Generate_WhenLengthDefined_ShouldReturnRandomSequenceOfDefinedLength()
        {
            // Arrange
            var generator = new RandomSequenceGenerator();

            // Act
            var result = generator.Generate(24);

            // Assert
            Assert.Equal(24, result.Length);
            Assert.Matches("^[A-Za-z0-9]+$", result);
        }
    }
}
