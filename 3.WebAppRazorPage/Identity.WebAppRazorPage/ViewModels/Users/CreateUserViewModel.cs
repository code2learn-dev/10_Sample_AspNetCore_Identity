namespace Identity.WebAppRazorPage.ViewModels.Users
{
    public class CreateUserViewModel : BaseCrudUserViewModel
    { 
        [Display(Name = "رمز عبور")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور را وارد کنید")]
		[StringLength(12, MinimumLength = 6, ErrorMessage = "رمز عبور باید بین 6 تا 12 حرف باشد")]
		public string Password { get; set; } = string.Empty;

		[Display(Name = "تکرار رمز عبور")]
		[Compare(nameof(Password), ErrorMessage = "رمز عبور با تکرار آن یکی نمی باشد")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
