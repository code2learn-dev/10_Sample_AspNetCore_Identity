using _10_Identity.WebApiApp.ViewModels.Categories;

namespace _10_Identity.WebApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, 
                              Type = typeof(IReadOnlyCollection<CategoryViewModel>))]
        public async Task<IActionResult> Get()
        {
            var appResult = await _categoryService.GetAllEntityDtosAsync();
            if (!appResult.IsSuccess) return BadRequest(appResult.Errors);

            IReadOnlyCollection<CategoryViewModel> categories =
                                mapper.Map<IReadOnlyCollection<CategoryViewModel>>((appResult.Result));        
            return Ok(categories);
        }
    }
}
