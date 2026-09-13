namespace Identity.ApplicationService.Account.Entites
{
    public class AccountProfileDtoModel
    {
		public string Id { get; set; } = string.Empty;

		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;

		public string UserName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public bool PhoneNumberConfirmed { get; set; }

		public bool EmailConfirmed { get; set; }

		public bool TwoFactorEnabled { get; set; }

		public string Image { get; set; } = string.Empty;

		public string RoleId { get; set; } = string.Empty; 
    }
}
