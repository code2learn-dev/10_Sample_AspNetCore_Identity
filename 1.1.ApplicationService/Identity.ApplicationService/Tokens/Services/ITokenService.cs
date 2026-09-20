
namespace Identity.ApplicationService.Tokens.Services
{
    public interface ITokenService
    {
        Task<ApplicationServiceResult<UserTokenDtoModel?>> GenerateTokenAsync(string userId);
       
        Task<ApplicationServiceResult<AccountDtoModel?>> 
            LoginAccountToGenerateTokenAsync(
            LoginDtoModel model, 
            AccountRole accountRole = AccountRole.member);
        
        Task<ApplicationServiceResult<bool>> ValidateToken(TokenValidatedContext? context);

        Task<ApplicationServiceResult<UserTokenDtoModel?>> RefreshTokenAsync(RefreshTokenDtoModel model);

        Task<ApplicationServiceResult<bool>> RevokeRefreshTokenAsync(string refreshToken);
    }
}
