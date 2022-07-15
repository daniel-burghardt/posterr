using Posterr.DTOs.Users;

namespace Posterr.DTOs.Posts
{
	public abstract class PostDto
	{
		public int PostId { get; set; }
		public UserDto User { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public string Type { get; set; }

		public class OriginalPostDto : PostDto
		{
			public string Content { get; set; }
		}

		public class QuotePostDto : PostDto
		{
			public string Content { get; set; }
			public PostDto QuotedPost { get; set; }
		}

		public class RepostDto : PostDto
		{
			public PostDto RepostedPost { get; set; }
		}
	}
}
