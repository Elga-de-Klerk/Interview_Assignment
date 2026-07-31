using Documents.Application.Common;

namespace Documents.Application.DocumentMutation
{
    public interface IDocumentMutationService
    {
        Task<Result<DocumentMutationResult>> Mutate(string fileName, Stream stream, CancellationToken cancellationToken);
    }
}
