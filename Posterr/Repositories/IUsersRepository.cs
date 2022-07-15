using Posterr.Models.Users;

namespace Posterr.Repositories
{
	public interface IUsersRepository
	{
		Task<User?> GetUser(int userId);
	}
}