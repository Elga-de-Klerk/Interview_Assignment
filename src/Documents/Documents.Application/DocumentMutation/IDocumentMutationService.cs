using Documents.Application.Common;

namespace Documents.Application.DocumentMutation
{
    /// <summary>
    /// Service that holds all document mutation behavior.
    /// </summary>
    public interface IDocumentMutationService
    {
        /// <summary>
        /// Mutates the uploaded document's content by appending a timestamp 
        /// and random character sequence.
        /// </summary>
        /// <param name="fileName">The original file's name.</param>
        /// <param name="stream">A readable stream of the file's contents.</param>
        /// <param name="cancellationToken">Request's abort signal if client disconnects.</param>
        /// <returns cref="Result<DocumentMutationResult>"></returns>
        Task<Result<DocumentMutationResult>> Mutate(string fileName, Stream stream, CancellationToken cancellationToken);
    }
}
