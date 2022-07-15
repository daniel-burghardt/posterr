using System.ComponentModel.DataAnnotations;

namespace Posterr.DTOs.Posts
{
	public class CreateOriginalPostDto
	{
		[MaxLength(777, ErrorMessage = "Post may not contain more than 777 characters")]
		public string Content { get; set; }
	}
}
