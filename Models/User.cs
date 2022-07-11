using System.ComponentModel.DataAnnotations;

namespace Posterr.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(14)]
		public string Username { get; set; }
	}
}
