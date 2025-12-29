using System.Reflection;
using Library.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Context;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Client> Clients => Set<Client>();
	public DbSet<Book> Books => Set<Book>();
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<OrderBook> OrderBooks => Set<OrderBook>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(
			Assembly.GetExecutingAssembly()
		);
	}
}
