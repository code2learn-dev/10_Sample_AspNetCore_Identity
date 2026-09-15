using Identity.Domain.IDentityContent;
using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repository.Users
{
    public class UserRepository : IUserRepository
	{
		private readonly AcademyDbContext _context;
		private readonly DbSet<UserToken> _userToken;

		public UserRepository(AcademyDbContext context)
		{
			_context = context;
			_userToken = _context.Set<UserToken>();

		}

		public async Task<UserToken?> AddUserToken(UserToken userToken)
		{
			await _userToken.AddAsync(userToken);
			int addedRows = await _context.SaveChangesAsync();
			return addedRows > 0 ? userToken : default;
		}
	}
}
