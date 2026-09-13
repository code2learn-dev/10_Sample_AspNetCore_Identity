using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.WebAppRazorPage.ViewModels.Users
{
    public class DeleteUserViewModel : UserViewModel
    {
		public string Email { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public bool PhoneNumberConfirmed { get; set; }

		public bool EmailConfirmed { get; set; }

		public string Image { get; set; } = string.Empty;

		public bool TwoFactorEnabled { get; set; }

        public IReadOnlyCollection<SelectListItem>? RoleSelectListItem { get; set; }

		public string UserImageUrl { get; set; } = string.Empty;

		public string RoleId { get; set; } = string.Empty;

    }
}
