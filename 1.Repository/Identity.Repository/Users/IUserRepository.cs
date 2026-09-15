using Identity.Domain.Users;

namespace Identity.Repository.Users
{
	public interface IUserRepository
	{
		Task<UserToken?> AddUserToken(UserToken userToken);
	}
}
