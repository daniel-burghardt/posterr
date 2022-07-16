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

		// QuotedPost and RepostedPost are objects rather than a PostDto's so that the Model Binder can properly serialize the derived types
		public class QuotePostDto : PostDto
		{
			public string Content { get; set; }
			public object QuotedPost { get; set; }
		}

		public class RepostDto : PostDto
		{
			public object RepostedPost { get; set; }
		}
	}
}
