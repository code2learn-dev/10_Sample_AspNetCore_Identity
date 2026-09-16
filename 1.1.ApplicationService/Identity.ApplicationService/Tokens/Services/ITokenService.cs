using Identity.ApplicationService.Tokens.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Identity.ApplicationService.Tokens.Services
{
    public interface ITokenService
    {
        Task<ApplicationServiceResult<UserTokenDtoModel?>> GenerateTokenAsync(UserTokenDtoModel? model);

        Task<ApplicationServiceResult<bool>> ValidateToken(TokenValidatedContext? context);
    }
}
