using Identity.ApplicationService.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace _10_Identity.WebApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// گرفتن رفرش توکی برای زمانی که توکن اصلی گرفته شده منقضی شده است
        /// در این صورت درخواست منقضی کردن رفرش توکن و ثبت توکن جدید را داریم
        /// </summary>
        /// <param name="model">
        /// مدل شاما مقادیر توکن جاری به همراه رفرش توکن
        /// تولید شده در زمان ایجاد توکن است</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationServiceResult<UserTokenDtoModel?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDtoModel model)
        {
            var tokenResult = await _tokenService.RefreshTokenAsync(model);
            if (!tokenResult.IsSuccess || tokenResult.Result is null)
                return Unauthorized(tokenResult);

            return Ok(tokenResult.Result);
        }

        /// <summary>
        /// در صدرتی که بخواهیم توکن به همراه ررش توکن فعلی کاربر را منقضی کنیم
        /// در این صورت کاربر مجددا باید لاگین کند
        /// </summary>
        /// <param name="refreshToken">ارسال رفرش توکن جاری که به همراه توکن فعال تولید شده شات</param>
        /// <returns></returns>
        [HttpPost("Revoke")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationServiceResult<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
        {
            var tokenResult = await _tokenService.RevokeRefreshTokenAsync(refreshToken);
            if (!tokenResult.IsSuccess || !tokenResult.Result)
                return BadRequest(tokenResult);

            return Ok(tokenResult);
        }
    }
}
