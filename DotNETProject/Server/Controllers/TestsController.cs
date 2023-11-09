using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNETProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "ROLE_ADMIN")]
        public IActionResult Get()
        {
            return Ok("Hello!");
        }
    }
}
