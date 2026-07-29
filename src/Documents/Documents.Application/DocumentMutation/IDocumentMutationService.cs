namespace Documents.Application.DocumentMutation
{
    public interface IDocumentMutationService
    {
        Task<DocumentMutationResult> Mutate(string fileName, Stream content);
    }
}
