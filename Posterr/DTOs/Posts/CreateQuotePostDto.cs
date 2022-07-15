using System.ComponentModel.DataAnnotations;

namespace Posterr.DTOs.Posts
{
	public class CreateQuotePostDto
	{
		[MaxLength(777, ErrorMessage = "Post may not contain more than 777 characters")]
		public string Content { get; set; }
		public int QuotedPostId { get; set; }
	}
}
