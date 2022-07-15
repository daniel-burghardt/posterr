using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posterr.Models.Posts
{
	public class QuotePost : Post
	{
		public int QuotedPostId { get; set; }

		[MaxLength(777)]
		public string Content { get; set; }
		
		[ForeignKey(nameof(QuotedPostId))]
		public Post QuotedPost { get; set; }
	}
}
