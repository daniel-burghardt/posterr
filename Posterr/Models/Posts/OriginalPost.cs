using System.ComponentModel.DataAnnotations;

namespace Posterr.Models.Posts
{
	public class OriginalPost : Post
	{
		[MaxLength(777)]
		public string Content { get; set; }
	}
}
