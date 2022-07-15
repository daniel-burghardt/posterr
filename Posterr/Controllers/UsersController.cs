using Microsoft.AspNetCore.Mvc;
using Posterr.Services;

namespace Posterr.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UsersService usersService;

		public UsersController(UsersService usersService)
		{
			this.usersService = usersService;
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetProfile(int userId)
		{
			var profileDto = await usersService.GetProfile(userId);
			return Ok(profileDto);
		}
	}
}
