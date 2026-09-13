using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.WebAppRazorPage.ViewModels.Users
{
    public class UpdatePasswordViewModel : UserViewModel
    {  
		[Display(Name = "رمز عبور جدید")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور جدید را وارد کنید")]
		[StringLength(12, MinimumLength = 6, ErrorMessage = "رمز عبور باید بین 6 تا 12 حرف باشد")]
		[RegularExpression(@"^[a-zA-Z\d!@#$%^&*.,()_={}\[\];:'""/<>]{6,12}$", ErrorMessage = "رمز عبور باید بین 6 تا 12 حرف باشد و باشد شامل حروف [a-zA-Z0-9] و حروف خاص تشکیل شده باشد")]
		public string NewPassword { get; set; } = string.Empty;

		[Display(Name = "تکرار رمز عبور")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور را تکرار کنید")]
		[Compare(nameof(NewPassword), ErrorMessage = "رمز عبور با تکرار آن یکی نمی باشد")]
		public string ConfirmPassword { get; set; } = string.Empty;
        
		public IReadOnlyCollection<SelectListItem>? RoleSelectListItem { get; set; }

		public string UserImageUrl { get; set; } = string.Empty;

		public string Image { get; set; } = string.Empty;

		public string RoleId { get; set; } = string.Empty;
    }
}
