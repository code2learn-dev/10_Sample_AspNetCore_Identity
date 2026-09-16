using System.ComponentModel.DataAnnotations;

namespace _10_Identity.WebApiApp.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد کنید")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور را وارد کنید")]
        public string Password { get; set; } = string.Empty;
    }
}
