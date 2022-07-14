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
			builder.Entity<User>().HasIndex(u => u.Username).IsUnique();

			builder.Entity<User>().HasData(new User() { Id = 1, Username = "user_one" });
			builder.Entity<User>().HasData(new User() { Id = 2, Username = "user_two" });
			builder.Entity<User>().HasData(new User() { Id = 3, Username = "user_three" });
		}
	}
}
