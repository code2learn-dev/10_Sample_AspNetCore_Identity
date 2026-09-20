
namespace Identity.ApplicationService.Account.Services
{
    public interface IAccountService
    {
        Task<ApplicationServiceResult<AccountProfileDtoModel?>> GetAccountProfileInfoAsync(); 

        Task<ApplicationServiceResult<AccountDtoModel?>> LoginAccountAsync(LoginDtoModel model);
       
        Task<ApplicationServiceResult<AccountDtoModel?>> LoginAccountToGenerateTokenAsync(
            LoginDtoModel model, 
            AccountRole accountRole = AccountRole.member);
      
        Task<ApplicationServiceResult<AccountDtoModel?>> RegisterAccountAsync(RegisterDtoModel model);

        Task<ApplicationServiceResult<AccountDtoModel?>> SignInManagerAdminAsync(LoginDtoModel model);
    }
}
