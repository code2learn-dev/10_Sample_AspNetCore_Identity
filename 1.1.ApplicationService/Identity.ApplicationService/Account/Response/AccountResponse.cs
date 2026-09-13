
namespace Identity.ApplicationService.Account.Response
{
    public class AccountResponse : IAccountResponse
    {
        ApplicationServiceResult<AccountDtoModel?> IAccountResponse.GetAccountResult()
            => new();

        ApplicationServiceResult<IReadOnlyCollection<AccountDtoModel>> IAccountResponse.GetAccountsList()
            => new();
    }
}
