using Documents.Application.DocumentMutation;
using Microsoft.AspNetCore.Mvc;

namespace Documents.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("multipart/form-data")]
    public class DocumentsController(
        IDocumentMutationService documentMutationService) : ControllerBase
    {
        [HttpPost("mutate")]
        public async Task<IActionResult> MutateDocument(IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null)
                return BadRequest("A file is required.");

            var result = await documentMutationService.Mutate(file.FileName, file.OpenReadStream(), cancellationToken);

            return result is { IsSuccess: true, Value: { } mutationResult } ? 
                File(mutationResult.Content, "text/plain", mutationResult.FileName) : 
                BadRequest(result.Error);
        }
    }
}
