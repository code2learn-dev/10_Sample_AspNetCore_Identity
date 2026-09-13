namespace Identity.ApplicationService.Account.Response
{
    public interface IAccountResponse
    {
        ApplicationServiceResult<AccountDtoModel?> GetAccountResult();

        ApplicationServiceResult<IReadOnlyCollection<AccountDtoModel>> GetAccountsList();
    }
}
