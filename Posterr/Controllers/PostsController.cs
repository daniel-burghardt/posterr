using Microsoft.AspNetCore.Mvc;
using Posterr.DTOs.Posts;
using Posterr.Services;

namespace Posterr.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly PostsService postsService;
		private readonly UsersService usersService;

		public PostsController(PostsService postsService, UsersService usersService)
		{
			this.postsService = postsService;
			this.usersService = usersService;
		}

		[HttpGet]
		public async Task<IActionResult> GetPosts([FromQuery] PostsQueryParametersDto queryParameters)
		{
			// We need to convert the list of PostDto's into a list of objects so that the Model Binder can serialize the derived types properly
			// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism
			var posts = await postsService.GetPosts(queryParameters);
			var postsObjects = posts.ToList<object>();

			return Ok(postsObjects);
		}

		[HttpPost("post")]
		public async Task<IActionResult> Post(CreateOriginalPostDto postDto)
		{
			try
			{
				var currentUser = await usersService.GetCurrentUser();
				var postId = await postsService.CreatePost(postDto, currentUser);

				return Ok(postId);
			}
			catch (ArgumentException e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost("quote")]
		public async Task<IActionResult> Quote(CreateQuotePostDto postDto)
		{
			try
			{
				var currentUser = await usersService.GetCurrentUser();
				var postId = await postsService.CreatePost(postDto, currentUser);

				return Ok(postId);
			}
			catch (ArgumentException e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost("repost")]
		public async Task<IActionResult> Repost(CreateRepostDto postDto)
		{
			try
			{
				var currentUser = await usersService.GetCurrentUser();
				var postId = await postsService.CreatePost(postDto, currentUser);

				return Ok(postId);
			}
			catch (ArgumentException e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
