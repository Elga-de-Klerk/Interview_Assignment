using Documents.Api.Controllers;
using Documents.Application.DocumentMutation;
using NSubstitute;

namespace Documents.Tests.Api.Fixtures
{
    internal class DocumentsControllerFixture
    {
        private readonly IDocumentMutationService _documentMutationService = Substitute.For<IDocumentMutationService>();

        public DocumentsControllerFixture SetupDocumentMutationServiceToReturn(DocumentMutationResult result)
        {
            _documentMutationService.Mutate(Arg.Any<string>(), Arg.Any<Stream>()).Returns(result);
            return this;
        }

        public void VerifyDocumentMutationServiceWasCalled(string fileName, Stream content)
        {
            _documentMutationService.Received(1).Mutate(fileName, content);
        }

        public DocumentsController Build() => new(_documentMutationService);
    }
}
