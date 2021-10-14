using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace library_api.V2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    public class AuthorController : ControllerBase
    {
        public AuthorController() { }

        [MapToApiVersion("2.0")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.FromResult("Message has returned from API V2"));
        }
    }
}
