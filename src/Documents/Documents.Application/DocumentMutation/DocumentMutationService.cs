using System.Reflection.Metadata;
using System.Text;

namespace Documents.Application.DocumentMutation
{
    public class DocumentMutationService : IDocumentMutationService
    {
        public async Task<DocumentMutationResult> Mutate(string fileName, Stream content)
        {
            using var reader = new StreamReader(content);

            var text = await reader.ReadToEndAsync();

            return new() 
            {
                FileName = fileName,
                Content = content 
            };
        }
    }
}
