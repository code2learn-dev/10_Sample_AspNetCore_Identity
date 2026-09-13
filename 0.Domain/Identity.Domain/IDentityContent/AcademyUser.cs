using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.IDentityContent
{
    public class AcademyUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
