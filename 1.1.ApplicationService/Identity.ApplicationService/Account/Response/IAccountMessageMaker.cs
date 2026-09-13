namespace Identity.ApplicationService.Account.Response
{
    public interface IAccountMessageMaker
    {
        string Message { get; }

        void SetMessage(
            CrudAccount crud, 
            AccountStatus status, 
            string message = "");
    }

    public enum CrudAccount : byte
    {
        login = 1,
        register,
        resetpassword
    }

    public enum AccountStatus : byte
    {
        ok = 1,
        locked,
        notfound,
        twofactor,
        error
    }
}
