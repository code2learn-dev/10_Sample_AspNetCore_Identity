using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.IDentityContent
{
    public class AcademyRole : IdentityRole
    {
        public string Description { get; set; } = string.Empty;
    }
}
