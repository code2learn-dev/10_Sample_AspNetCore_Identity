using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.WebAppRazorPage.ViewModels.Account
{
    public class AccountProfileViewModel
    {
		public string Id { get; set; } = string.Empty;

		[Display(Name = "نام")]
		public string FirstName { get; set; } = string.Empty;

		[Display(Name = "نام خانوادگی")]
		public string LastName { get; set; } = string.Empty;

		[Display(Name = "نام کاربری")]
		public string UserName { get; set; } = string.Empty;

		[Display(Name = "آدرس ایمیل")]
		public string Email { get; set; } = string.Empty;

		[Display(Name = "شماره تماس")]
		public string PhoneNumber { get; set; } = string.Empty;

		[Display(Name = "وضعیت تایید شماره تماس")]
		public bool PhoneNumberConfirmed { get; set; }

		[Display(Name = "وضعیت تایید آدرس ایمیل")]
		public bool EmailConfirmed { get; set; }

		[Display(Name = "ورود دو مرحله ای")]
		public bool TwoFactorEnabled { get; set; }

		public string Image { get; set; } = string.Empty;

		[Display(Name = "نقش تعریف شده")]
		public string RoleId { get; set; } = string.Empty;

        public IReadOnlyCollection<SelectListItem>? RolesSelectListItem { get; set; }

        public string ImageUrl { get; set; } = "https://localhost:7286/assets/images/users";
    }
}
