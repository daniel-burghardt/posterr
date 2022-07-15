using Posterr.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posterr.Models.Posts
{
	public abstract class Post
	{
		public int PostId { get; set; }
		public int UserId { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public string Discriminator { get; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
	}
}
