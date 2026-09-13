using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Identity.ApplicationService.Account.Response
{
    public class AccountMessageMaker : IAccountMessageMaker
    {
        private string _message = string.Empty;

        public string Message => _message;

        public void SetMessage(CrudAccount crud, AccountStatus status, string message = "")
        {
            _message = crud switch
            {
                CrudAccount.login => status switch
                {
                    AccountStatus.locked => "حساب کاربری شما به مدت 20 ثانیه قفل شده است",
                    AccountStatus.notfound => "نام کاربری و رمز عبور نامعتبر می باشد", 
                    AccountStatus.error => "خطایی در زمان ورود کاربر رخ داده است",
                    AccountStatus.twofactor => "ورود دو مرحله ای برای شما فعال شده است",
                    _ => message
                },

                CrudAccount.register => status switch
                {
                    AccountStatus.error => message,
                    AccountStatus.ok => "ثبت نام شما با موفقیت انجام شد",
                    _ => message
                },

                _ => string.Empty
            };
        }
    }

}
