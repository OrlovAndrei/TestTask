using Microsoft.EntityFrameworkCore;
using TestTask.Domain;

namespace TestTask.Infrastructure
{
	public class AppDbContext : DbContext
	{
		public DbSet<Employee> Employees { get; set; }

		public AppDbContext(DbContextOptions options) : base(options) {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Employee>().HasKey(e => e.Id);
			modelBuilder.Entity<Employee>().HasAlternateKey(e => e.FullName);
			modelBuilder.Entity<Employee>().Property(e => e.FullName).IsRequired();
		}
	}
}