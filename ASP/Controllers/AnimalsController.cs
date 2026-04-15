using Microsoft.AspNetCore.Mvc;

namespace ASP.Controllers
{
    // api/animals
    [Route("api/[controller]")]
    [ApiController]
    
    public class AnimalsController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}