using Document.Api.Tests.Builders;
using Document.Api.Tests.Fixtures;
using Documents.Application.DocumentMutation;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Document.Api.Tests.Controllers
{
    public class DocumentsControllerTests
    {
        [Fact]
        public async Task MutateDocument_WhenInputIsValid_ShouldReturnMutatedFile()
        {
            // Arrange
            var fixture = new DocumentsControllerFixture();
            var fileName = "filename.txt";
            var content = "File content";
            var file = new FormFileBuilder()
                .WithFileName(fileName)
                .WithContent(content)
                .Build();

            var mutatedContent = "Mutated content";
            var mutationResult = new DocumentMutationResult() { FileName = fileName, Content = Encoding.UTF8.GetBytes(mutatedContent) };

            var controller = fixture
                .SetupDocumentMutationServiceToReturn(mutationResult)
                .Build();

            // Act
            var result = await controller.MutateDocument(file);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal(fileName, fileResult.FileDownloadName);
            Assert.Equal("text/plain", fileResult.ContentType);

            var fileResultContentAsString = Encoding.UTF8.GetString(fileResult.FileContents);
            Assert.Equal(mutatedContent, fileResultContentAsString);

            fixture.VerifyDocumentMutationServiceWasCalled(fileName, file.OpenReadStream());
        }

        [Fact]
        public async Task MutateDocument_WhenFileIsNull_ShouldReturnBadRequest()
        {
            // Arrange
            var fixture = new DocumentsControllerFixture();
            var controller = fixture.Build();

            // Act
            var result = await controller.MutateDocument(null!);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("A file is required.", badRequestResult.Value);
        }
    }
}
