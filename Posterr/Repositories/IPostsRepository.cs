using Posterr.DTOs.Posts;
using Posterr.Models.Posts;

namespace Posterr.Repositories
{
	public interface IPostsRepository
	{
		Task<int> Create(Post post);
		Task<Post?> GetPost(int postId);
		Task<List<Post>> GetPosts(PostsQueryParametersDto queryParameters);
		Task<int> TotalPostsByUser(int userId, DateOnly? date = null);
	}
}