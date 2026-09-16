namespace _10_Identity.WebApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase   
    {
        private readonly UserManager<AcademyUser> _userManager;
        private readonly SignInManager<AcademyRole> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginController(
            UserManager<AcademyUser> userManager,
            SignInManager<AcademyRole> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserTokenDtoModel))]
        public async Task<IActionResult> Get()
        {

            return Ok();
        }
    }
}
