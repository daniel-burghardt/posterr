namespace Posterr.DTOs.Posts
{
	public class PostsQueryParametersDto : PageDto
	{
		public int? UserId { get; set; }
		public DateTimeOffset? StartDate { get; set; }
		public DateTimeOffset? EndDate { get; set; }
	}
}
