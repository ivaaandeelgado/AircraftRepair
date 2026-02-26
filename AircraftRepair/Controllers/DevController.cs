using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AircraftRepair.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevController : ControllerBase
    {
        [HttpGet("generate-hash")]
        public IActionResult GenerateHash()
        {
            var hasher = new PasswordHasher<object>();
            var hash = hasher.HashPassword(null, "Admin123!");

            return Ok(hash);
        }
    }
}
