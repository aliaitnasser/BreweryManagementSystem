using Models;

using Persistence;

using System;
using System.Linq;

namespace Application.Repositories
{
	public class WholesalerRepository : IWholesalerRepository
	{
		private readonly DataContext _context;

		public WholesalerRepository(DataContext context)
		{
			_context = context;
		}
		public async Task AddSale(WholesalerBeer wholesalerBeer)
		{
			var beer = await _context.Beers.FindAsync(wholesalerBeer.BeerId);
			if (beer == null) throw new Exception("Beer must existe");

			var wholesaler = await _context.Wholesalers.FindAsync(wholesalerBeer.WholesalerId);
			if (wholesaler == null) throw new Exception("Wholesaler must existe");

			if (beer.Quantity < wholesalerBeer.Quantity) throw new Exception("Beer quantity is not enaugh");

			beer.Quantity -= wholesalerBeer.Quantity;

			_context.Beers.Update(beer);
			_context.WholesalerBeers.Add(wholesalerBeer);

			await _context.SaveChangesAsync();
		}

		public async Task UpdateRemainingStock(int wholesalerBeerId, int stock)
		{
			var wholesalerBeer = await _context.WholesalerBeers.FindAsync(wholesalerBeerId);
			if (wholesalerBeer == null) throw new Exception("Wholesalerbeer dont existe");

			wholesalerBeer.Quantity = stock;

			_context.WholesalerBeers.Update(wholesalerBeer);
			await _context.SaveChangesAsync();
		}
	}
}
