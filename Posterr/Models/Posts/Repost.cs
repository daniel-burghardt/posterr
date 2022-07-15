using System.ComponentModel.DataAnnotations.Schema;

namespace Posterr.Models.Posts
{
	public class Repost : Post
	{
		public int RepostedPostId { get; set; }

		[ForeignKey(nameof(RepostedPostId))]
		public Post RepostedPost { get; set; }
	}
}
