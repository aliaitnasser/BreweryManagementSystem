using Microsoft.EntityFrameworkCore;
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
		public async Task AddSale(BeerStock beerStock)
		{
			var beer = await _context.Beers.FindAsync(beerStock.BeerId);
			if (beer == null) throw new Exception("Beer must existe");

			var wholesaler = await _context.Wholesalers.FindAsync(beerStock.WholesalerId);
			if (wholesaler == null) throw new Exception("Wholesaler must existe");

			if (beer.Quantity < beerStock.Quantity) throw new Exception("Beer quantity is not enaugh");

			beer.Quantity -= beerStock.Quantity;

			_context.Beers.Update(beer);
			_context.BeerStocks.Add(beerStock);

			await _context.SaveChangesAsync();
		}

		public async Task UpdateRemainingStock(int beerStockId ,int wholesalerId, int quantity)
		{
			var beerStock = await _context.BeerStocks.FindAsync(beerStockId);
			if (beerStock == null) throw new Exception("BeerStock don't exist");
			if(beerStock.WholesalerId != wholesalerId) throw new Exception("BeerStock don't belong to this wholesaler");
			beerStock.Quantity = quantity;
			_context.BeerStocks.Update(beerStock);
			await _context.SaveChangesAsync();
		}

		public async Task<List<BeerStock>> GetAllStockByWholesaler(int wholesalerId)
		{
			var beerStocks = await _context.BeerStocks.Where(x => x.WholesalerId == wholesalerId).ToListAsync();
			if (beerStocks == null) throw new Exception("Wholesaler don't exist");
			return beerStocks;
		}
	}
}
