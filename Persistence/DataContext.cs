using Microsoft.EntityFrameworkCore;

using Models;

using System;
using System.Linq;

namespace Persistence
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options) { }

		public DbSet<Beer> Beers { get; set; }
		public DbSet<Brewery> Breweries { get; set; }
		public DbSet<Wholesaler> Wholesalers { get; set; }
		public DbSet<BeerStock> BeerStocks { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<BeerStock>()
				.HasOne(bs => bs.Wholesaler)
				.WithMany(b => b.BeerStocks)
				.HasForeignKey(bs => bs.WholesalerId);

			modelBuilder.Entity<Beer>()
				.HasOne(b => b.Brewery)
				.WithMany(b => b.Beers)
				.HasForeignKey(b => b.BreweryId);

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Wholesaler)
				.WithMany(w => w.Orders)
				.HasForeignKey(o => o.WholesalerId);
		}
	}
}
