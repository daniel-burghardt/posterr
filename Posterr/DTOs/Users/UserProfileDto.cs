namespace Posterr.DTOs.Users
{
	public class UserProfileDto
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string JoinedOn { get; set; }
		public int TotalPosts { get; set; }
	}
}
