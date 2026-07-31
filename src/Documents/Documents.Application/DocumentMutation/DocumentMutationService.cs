using Documents.Application.Abstractions;
using Documents.Application.Common;
using Documents.Domain;
using System.Text;
using System.Text.Unicode;

namespace Documents.Application.DocumentMutation
{
    public class DocumentMutationService(
        IDateTimeProvider dateTimeProvider,
        IRandomSequenceGenerator randomSequenceGenerator) : IDocumentMutationService
    {
        public async Task<Result<DocumentMutationResult>> Mutate(string fileName, Stream stream, CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream, cancellationToken);
            var bytes = memoryStream.ToArray();

            if (!Utf8.IsValid(bytes))
                return Result<DocumentMutationResult>.Failure("The uploaded file does not contain valid text content.");

            var content = Encoding.UTF8.GetString(bytes);
            var document = new TextDocument(fileName, content);
            var mutatedDocument = document.Mutate(dateTimeProvider.CurrentDateTime, randomSequenceGenerator.Generate());

            var mutationResult = new DocumentMutationResult
            {
                FileName = mutatedDocument.FileName,
                Content = Encoding.UTF8.GetBytes(mutatedDocument.Content)
            };

            return Result<DocumentMutationResult>.Success(mutationResult);
        }
    }
}
