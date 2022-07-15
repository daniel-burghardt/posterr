using Microsoft.EntityFrameworkCore;
using Posterr.Models;
using Posterr.Models.Users;

namespace Posterr.Repositories
{
	public class UsersRepository
	{
		public PosterrDbContext dbContext;

		public UsersRepository(PosterrDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<User?> GetUser(int userId)
		{
			return await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
		}
	}
}
