using AutoMapper;
using Moq;
using NUnit.Framework;
using Posterr.DTOs.Posts;
using Posterr.Helpers;
using Posterr.Models.Posts;
using Posterr.Models.Users;
using Posterr.Repositories;
using Posterr.Services;

namespace PosterrTests
{
	public class Tests
	{
		private Mock<IPostsRepository> postsRepository;
		private PostsService postsService;

		[SetUp]
		public void Setup()
		{
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapperProfile());
			});
			var mapper = mockMapper.CreateMapper();

			postsRepository = new Mock<IPostsRepository>();
			postsService = new PostsService(postsRepository.Object, mapper);
		}

		[Test]
		public void OriginalPost()
		{
			Assert.DoesNotThrowAsync(async () => await postsService.CreatePost(new CreateOriginalPostDto()
			{
				Content = "Hi!"
			}, new User() { UserId = 1, Username = "one" }));
		}

		[Test]
		public void PostsLimitReached()
		{
			var currentUser = new User() { UserId = 1, Username = "one" };

			postsRepository.Setup(x => x.TotalPostsByUser(currentUser.UserId, DateOnly.FromDateTime(DateTime.Now))).ReturnsAsync(5);

			Assert.ThrowsAsync<Exception>(async () => await postsService.CreatePost(new CreateOriginalPostDto()
			{
				Content = "Hi!"
			}, currentUser), "User has reached the limit of posts per day");
		}

		[Test]
		public void QuoteExceptionAnotherQuote()
		{
			var existingQuote = new QuotePost() { PostId = 1 };
			postsRepository.Setup(x => x.GetPost(existingQuote.PostId)).ReturnsAsync(existingQuote);

			Assert.ThrowsAsync<ArgumentException>(async () => await postsService.CreatePost(new CreateQuotePostDto()
			{
				Content = "Hi!",
				QuotedPostId = existingQuote.PostId,
			}, new User() { UserId = 1, Username = "one" }), "Quote posts may not be quoted");
		}

		[Test]
		public void QuoteExceptionSameUser()
		{
			var currentUser = new User() { UserId = 1, Username = "one" };
			var existingPost = new OriginalPost() { PostId = 1, UserId = currentUser.UserId };
			postsRepository.Setup(x => x.GetPost(existingPost.PostId)).ReturnsAsync(existingPost);

			Assert.ThrowsAsync<ArgumentException>(async () => await postsService.CreatePost(new CreateQuotePostDto()
			{
				Content = "Hi!",
				QuotedPostId = existingPost.PostId,
			}, currentUser), "May not quote own post");
		}
	}
}