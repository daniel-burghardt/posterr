using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Posterr.Models.Users
{
	[Index(nameof(Username), IsUnique = true)]
	public class User
	{
		[Key]
		public int UserId { get; set; }
		[MaxLength(14)]
		public string Username { get; set; }
		public DateTimeOffset JoinedOn { get; set; }
	}
}
