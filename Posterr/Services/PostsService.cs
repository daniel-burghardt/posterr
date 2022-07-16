using AutoMapper;
using Posterr.DTOs.Posts;
using Posterr.Models.Posts;
using Posterr.Models.Users;
using Posterr.Repositories;

namespace Posterr.Services
{
	public class PostsService
	{
		private readonly IPostsRepository postsRepository;
		private readonly IMapper mapper;

		public PostsService(IPostsRepository postsRepository, IMapper mapper)
		{
			this.postsRepository = postsRepository;
			this.mapper = mapper;
		}

		public async Task<List<PostDto>> GetPosts(PostsQueryParametersDto queryParameters)
		{
			var posts = await postsRepository.GetPosts(queryParameters);
			var postsDtos = mapper.Map<List<PostDto>>(posts);

			return postsDtos;
		}

		public async Task<int> CreatePost(CreateOriginalPostDto postDto, User user)
		{
			return await CreatePost<OriginalPost>(postDto, user);
		}

		public async Task<int> CreatePost(CreateQuotePostDto postDto, User user)
		{
			var quotedPost = await postsRepository.GetPost(postDto.QuotedPostId);
			if (quotedPost is QuotePost)
				throw new ArgumentException("Quote posts may not be quoted");
			if (quotedPost.UserId == user.UserId)
				throw new ArgumentException("May not quote own post");

			return await CreatePost<QuotePost>(postDto, user);
		}

		public async Task<int> CreatePost(CreateRepostDto postDto, User user)
		{
			var repostedPost = await postsRepository.GetPost(postDto.RepostedPostId);
			if (repostedPost is Repost)
				throw new ArgumentException("Repost posts may not be reposted");
			if (repostedPost.UserId == user.UserId)
				throw new ArgumentException("May not repost own post");

			return await CreatePost<Repost>(postDto, user);
		}

		private async Task<int> CreatePost<T>(object postDto, User user) where T : Post
		{
			var postsToday = await postsRepository.TotalPostsByUser(user.UserId, DateOnly.FromDateTime(DateTime.Now));
			if (postsToday >= 5)
				throw new ArgumentException("User has reached the limit of posts per day");

			var post = mapper.Map<T>(postDto);
			post.UserId = user.UserId;
			post.CreatedAt = DateTimeOffset.Now;

			await postsRepository.Create(post);

			return post.PostId;
		}
	}
}
