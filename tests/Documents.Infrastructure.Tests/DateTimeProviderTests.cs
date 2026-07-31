using Documents.Infrastructure;

namespace Documents.Tests.Infrastructure
{
    public class DateTimeProviderTests
    {
        [Fact]
        public void CurrentDateTime_WhenCalled_ShouldReturnDateTimeOffsetNow()
        {
            // Arrange
            var provider = new DateTimeProvider();

            // Act
            var result = provider.CurrentDateTime;

            // Assert
            Assert.IsType<DateTimeOffset>(result);
            Assert.Equal(DateTimeOffset.Now, result, TimeSpan.FromSeconds(1));
        }
    }
}
