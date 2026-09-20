using Identity.Domain.Users;

namespace Identity.Repository.Users
{
	public interface IUserRepository
	{
		Task<UserToken?> AddUserToken(UserToken userToken);

		Task<UserToken?> UpdateUserTokenAsync(UserToken userToken);

		Task<bool> RevokeAllUserTokensAsync(string userId);
	}
}
