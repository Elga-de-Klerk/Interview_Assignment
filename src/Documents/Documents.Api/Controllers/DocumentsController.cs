using Microsoft.AspNetCore.Mvc;

namespace Documents.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult MutateDocument()
        {
            throw new NotImplementedException();
        }
    }
}
