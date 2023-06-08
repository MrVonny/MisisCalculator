using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace CalcaulatorBackend.Infrastrucure;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<History> Histories { get; set; }
}