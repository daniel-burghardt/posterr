using Microsoft.EntityFrameworkCore;
using Posterr.DTOs.Posts;
using Posterr.Models;
using Posterr.Models.Posts;

namespace Posterr.Repositories
{
	public class PostsRepository
	{
		public PosterrDbContext dbContext;

		public PostsRepository(PosterrDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<List<Post>> GetPosts(PostsQueryParametersDto queryParameters)
		{
			return await dbContext.Posts
				.Where(x => queryParameters.UserId == null || x.UserId == queryParameters.UserId)
				.Where(x => queryParameters.StartDate == null || x.CreatedAt >= queryParameters.StartDate)
				.Where(x => queryParameters.EndDate == null || x.CreatedAt <= queryParameters.EndDate)
				.Include(x => x.User)
				.Include(x => ((Repost)x).RepostedPost)
				.Include(x => ((QuotePost)x).QuotedPost)
				// If the user base grows, consider using a more performant type of pagination (e.g.: keyset pagination)
				.Skip(queryParameters.PageSize * (queryParameters.CurrentPage - 1))
				.Take(queryParameters.PageSize)
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync();
		}

		public async Task<Post?> GetPost(int postId)
		{
			return await dbContext.Posts.SingleOrDefaultAsync(x => x.PostId == postId);
		}

		public async Task<int> Create(Post post)
		{
			await dbContext.Posts.AddAsync(post);
			await dbContext.SaveChangesAsync();
			return post.PostId;
		}

		public async Task<int> TotalPostsByUser(int userId, DateOnly? date = null)
		{
			return await dbContext.Posts
				.Where(x => x.UserId == userId)
				.Where(x => date == null || x.CreatedAt.Year == date.Value.Year && x.CreatedAt.Month == date.Value.Month && x.CreatedAt.Day == date.Value.Day)
				.CountAsync();
		}
	}
}
