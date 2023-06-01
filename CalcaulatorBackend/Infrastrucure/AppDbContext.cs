using Microsoft.EntityFrameworkCore;

namespace CalcaulatorBackend.Infrastrucure;

public class AppDbContext : DbContext
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		Database.EnsureCreated();
	}

	public DbSet<History> Histories { get; set; }
}

public class History
{
	public List<string> Expressions { get; set; }
}