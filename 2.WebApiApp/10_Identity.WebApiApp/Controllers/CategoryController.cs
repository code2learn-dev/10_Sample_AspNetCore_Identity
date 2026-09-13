namespace _10_Identity.WebApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Category List");
        }
    }
}
