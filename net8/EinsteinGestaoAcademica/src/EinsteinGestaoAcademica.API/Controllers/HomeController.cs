using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EinsteinGestaoAcademica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello, World!");
        }
    }
}
