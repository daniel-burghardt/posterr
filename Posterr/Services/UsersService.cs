using AutoMapper;
using Posterr.DTOs.Users;
using Posterr.Models.Users;
using Posterr.Repositories;

namespace Posterr.Services
{
	public class UsersService
	{
		private readonly IUsersRepository usersRepository;
		private readonly IPostsRepository postsRepository;
		private readonly IMapper mapper;

		public UsersService(IUsersRepository usersRepository, IPostsRepository postsRepository, IMapper mapper)
		{
			this.usersRepository = usersRepository;
			this.postsRepository = postsRepository;
			this.mapper = mapper;
		}

		public async Task<User?> GetCurrentUser()
		{
			// Hardcoded first user
			return await usersRepository.GetUser(1);
		}

		public async Task<UserProfileDto> GetProfile(int userId)
		{
			var user = await usersRepository.GetUser(userId);
			var userProfileDto = mapper.Map<UserProfileDto>(user);
			userProfileDto.TotalPosts = await postsRepository.TotalPostsByUser(userId);

			return userProfileDto;
		}
	}
}
