namespace Identity.WebAppRazorPage.ViewModels.Account
{
    public class RegisterViewModel : BaseViewModel
    {
        [Display(Name = "آدرس ایمیل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "آدرس ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل را وارد کنید")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد کنید")]
        public string UserName { get; set; } = string.Empty;

		[Display(Name = "رمز عبور")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور را وارد کنید")] 
        public string Password { get; set; } = string.Empty;

		[Display(Name = "تکرار رمز عبور")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "تکرار رمز عبور را وارد کنید")]
        [Compare(nameof(Password), ErrorMessage = "تکرار رمز عبور با رمز عبور یکسان نمی باشد")]
		public string ConfirmPassword { get; set; } = string.Empty;
    }
}
