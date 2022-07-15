using Microsoft.EntityFrameworkCore;
using Posterr.Models.Posts;
using Posterr.Models.Users;

namespace Posterr.Models
{
	public class PosterrDbContext : DbContext
	{
		public PosterrDbContext(DbContextOptions<PosterrDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<OriginalPost> OriginalPosts { get; set; }
		public DbSet<QuotePost> QuotePosts { get; set; }
		public DbSet<Repost> Reposts { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<User>().HasData(new User() { UserId = 1, Username = "user_one", JoinedOn = new DateTimeOffset(2022, 07, 15, 10, 0, 0, new TimeSpan(-3, 0, 0)) });
			builder.Entity<User>().HasData(new User() { UserId = 2, Username = "user_two", JoinedOn = new DateTimeOffset(2022, 07, 16, 10, 0, 0, new TimeSpan(-3, 0, 0)) });
			builder.Entity<User>().HasData(new User() { UserId = 3, Username = "user_three", JoinedOn = new DateTimeOffset(2022, 07, 17, 10, 0, 0, new TimeSpan(-3, 0, 0)) });
			builder.Entity<User>().HasData(new User() { UserId = 4, Username = "user_four", JoinedOn = new DateTimeOffset(2022, 07, 18, 10, 0, 0, new TimeSpan(-3, 0, 0)) });
		}
	}
}
