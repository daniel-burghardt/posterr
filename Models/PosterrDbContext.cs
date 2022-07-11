using Microsoft.EntityFrameworkCore;

namespace Posterr.Models
{
	public class PosterrDbContext : DbContext
	{
		public PosterrDbContext(DbContextOptions<PosterrDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
	}
}
