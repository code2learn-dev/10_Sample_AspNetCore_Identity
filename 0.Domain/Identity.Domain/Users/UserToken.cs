using Identity.Domain.Common;

namespace Identity.Domain.Users
{
    public class UserToken : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;  
        public bool IsActive { get; set; } = true;
		public string RefreshToken { get; set; } = string.Empty;
		public DateTime ExpireRefreshToken { get; set; }
		public string DeviceName { get; set; } = string.Empty;
	}
}
