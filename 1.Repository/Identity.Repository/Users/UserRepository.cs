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

        public async Task<bool> RevokeAllUserTokensAsync(string userId)
        {
            IQueryable<UserToken> userTokens = _userToken.Where(a => a.UserId == userId);
			foreach (var userToken in userTokens)
			{
				userToken.IsActive = false;
				_userToken.Update(userToken);
			}

			return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserToken?> UpdateUserTokenAsync(UserToken userToken)
        {
			_userToken.Update(userToken);
			int updatedTokens = await _context.SaveChangesAsync();
			return updatedTokens > 0 ? userToken : default;
        }
    }
}
