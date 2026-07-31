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
        /// <summary>
        /// Allows the user to upload a text file and returns a copy with 
        /// the current time and a random character sequence appended.
        /// </summary>
        /// <param name="file">The file to be mutated.</param>
        /// <param name="cancellationToken">Request's abort signal if client disconnects.</param>
        /// <returns cref="Task<IActionResult>></returns>
        [HttpPost("mutate")]
        public async Task<IActionResult> MutateDocument(IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null)
                return BadRequest("A file is required.");

            var result = await documentMutationService.Mutate(file.FileName, file.OpenReadStream(), cancellationToken);

            return result is { IsSuccess: true, Content: { } mutationResult } ? 
                File(mutationResult.Content, "text/plain", mutationResult.FileName) : 
                BadRequest(result.Error);
        }
    }
}
