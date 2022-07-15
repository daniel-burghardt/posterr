using Microsoft.AspNetCore.Mvc;
using Posterr.DTOs.Posts;
using Posterr.Models.Posts;
using Posterr.Services;
using System.Text.Json;

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
			// We need to convert the list of PostDto's into a list of objects so that the Model Binder can serialize the polymorphic types properly			
			var posts = await postsService.GetPosts(queryParameters);
			var postsObjects = posts.ToList<object>();
			// 3rd level deep dont contain derived props

			return Ok(postsObjects);
		}

		[HttpPost("post")]
		public async Task<IActionResult> Post(CreateOriginalPostDto postDto)
		{
			var currentUser = await usersService.GetCurrentUser();
			var postId = await postsService.CreatePost(postDto, currentUser);

			return Ok(postId);
		}

		[HttpPost("quote")]
		public async Task<IActionResult> Quote(CreateQuotePostDto postDto)
		{
			var currentUser = await usersService.GetCurrentUser();
			var postId = await postsService.CreatePost(postDto, currentUser);

			return Ok(postId);
		}

		[HttpPost("repost")]
		public async Task<IActionResult> Repost(CreateRepostDto postDto)
		{
			var currentUser = await usersService.GetCurrentUser();
			var postId = await postsService.CreatePost(postDto, currentUser);

			return Ok(postId);
		}
	}
}
