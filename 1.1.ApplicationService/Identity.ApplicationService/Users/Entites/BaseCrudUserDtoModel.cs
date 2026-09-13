namespace Identity.ApplicationService.Users.Entites
{
    public abstract class BaseCrudUserDtoModel : UserDtoModel
    { 

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool PhoneNumberConfirmed { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string Image { get; set; } = string.Empty;

        public string RoleId { get; set; } = string.Empty;

    }
}
