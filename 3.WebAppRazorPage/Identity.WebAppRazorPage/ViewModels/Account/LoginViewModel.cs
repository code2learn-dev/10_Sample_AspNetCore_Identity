namespace Identity.WebAppRazorPage.ViewModels.Account
{
    public class LoginViewModel
    {
		[Display(Name = "نام کاربری")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد کنید")]
		public string UserName { get; set; } = string.Empty;

		[Display(Name = "رمز عبور")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور را وارد کنید")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		[Display(Name = "ذخیره رمز عبور")]
        public bool RememberMe { get; set; }

		public string? ReturnUrl { get; set; }
    }
}
