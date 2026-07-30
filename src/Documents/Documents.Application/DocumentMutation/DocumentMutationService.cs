using Documents.Application.Abstractions;
using Documents.Domain;
using System.Text;

namespace Documents.Application.DocumentMutation
{
    public class DocumentMutationService(
        IDateTimeProvider dateTimeProvider,
        IRandomSequenceGenerator randomSequenceGenerator) : IDocumentMutationService
    {
        public async Task<DocumentMutationResult> Mutate(string fileName, Stream content)
        {
            using var reader = new StreamReader(content);

            var text = await reader.ReadToEndAsync();
            var document = new TextDocument(fileName, text);
            var mutatedDocument = document.Mutate(dateTimeProvider.CurrentDateTime, randomSequenceGenerator.Generate());

            return new() 
            {
                FileName = mutatedDocument.FileName,
                Content = Encoding.UTF8.GetBytes(mutatedDocument.Content)
            };
        }
    }
}
