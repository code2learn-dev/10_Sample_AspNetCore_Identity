using _10_Identity.WebApiApp.Utilities;
using _10_Identity.WebApiApp.ViewModels.Accounts;
using Identity.ApplicationService.Account.Entites; 
using Identity.ApplicationService.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace _10_Identity.WebApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase   
    { 
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public LoginController( 
            ITokenService tokenService,
            IMapper mapper)
        { 
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserTokenDtoModel))]
        public async Task<IActionResult> Get(LoginViewModel model)
        {
            ApplicationServiceResult<UserTokenDtoModel> appResult = new();

            if(!ModelState.IsValid)
            {
                appResult.AddErrorsList(ModelState.GetModelStateErros());
                return BadRequest(appResult);
            }

            var loginResult = await _tokenService.LoginAccountToGenerateTokenAsync(_mapper.Map<LoginDtoModel>(model));
            if(!loginResult.IsSuccess || loginResult.Result is null)
            {
                appResult.AddErrorsList([.. loginResult.Errors]);
                return BadRequest(appResult);
            }

            var tokenResult = await _tokenService.GenerateTokenAsync(loginResult.Result.Id);
            if(!tokenResult.IsSuccess || tokenResult.Result is null)
            {
                appResult.AddErrorsList([.. tokenResult.Errors]);
                return BadRequest(appResult);
            }

            appResult.AddResult(tokenResult.Result);
            return Ok(appResult);
        }
    }
}
