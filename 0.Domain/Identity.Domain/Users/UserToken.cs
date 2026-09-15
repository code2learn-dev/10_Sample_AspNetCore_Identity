using Identity.Domain.Common;

namespace Identity.Domain.Users
{
    public class UserToken : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
