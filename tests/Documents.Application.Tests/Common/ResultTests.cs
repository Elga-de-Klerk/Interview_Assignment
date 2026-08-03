using Documents.Application.Common;

namespace Documents.Tests.Application.Common
{
    public class ResultTests
    {
        [Fact]
        public void Success_WhenCalled_ShouldReturnSuccesResult()
        {
            // Arrange
            var content = "Success";

            // Act
            var result = Result<string>.Success(content);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(content, result.Content);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Failure_WhenCalled_ShouldReturnFailureResult()
        {
            // Arrange
            var error = "Error";

            // Act
            var result = Result<string>.Failure(error);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Content);
            Assert.Equal(error, result.Error);
        }
    }
}
