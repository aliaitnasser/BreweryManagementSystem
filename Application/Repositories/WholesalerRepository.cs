using Application.Core;
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
		public async Task<Result<BeerStock>> AddSale(BeerStock beerStock)
		{
			var beer = await _context.Beers.FirstOrDefaultAsync(x => x.Id == beerStock.BeerId);
			if (beer == null) return Result<BeerStock>.Failure("Beer must exist");

			var wholesaler = await _context.Wholesalers.FirstOrDefaultAsync(x => x.Id == beerStock.WholesalerId);
			if (wholesaler == null) return Result<BeerStock>.Failure("Wholesaler must exist");

			if (beer.Quantity < beerStock.Quantity) return Result<BeerStock>.Failure("Quantity must be less than or equal to the remaining stock");

			beer.Quantity -= beerStock.Quantity;

			_context.Beers.Update(beer);

			var beerStockExist = _context.BeerStocks.FirstOrDefault(x => x.BeerId == beerStock.BeerId && x.WholesalerId == beerStock.WholesalerId);
			if (beerStockExist != null)
			{
				beerStockExist.Quantity += beerStock.Quantity;
				_context.BeerStocks.Update(beerStockExist);
			}
			else
			{
				_context.BeerStocks.Add(beerStock);
			}
			
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Result<BeerStock>.Success(beerStock, "Sale added successfully");
			return Result<BeerStock>.Failure("Failed to add sale");
		}

		public async Task<Result<BeerStock>> UpdateRemainingStock(int beerStockId ,int wholesalerId, int quantity)
		{
			var beerStock = await _context.BeerStocks.FirstOrDefaultAsync(x => x.Id == beerStockId);
			if (beerStock == null) return Result<BeerStock>.Failure("BeerStock does not exist");
			if(beerStock.WholesalerId != wholesalerId) return Result<BeerStock>.Failure("BeerStock does not belong to this wholesaler");
			
			beerStock.Quantity = quantity;
			_context.BeerStocks.Update(beerStock);
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Result<BeerStock>.Success(beerStock, "Remaining stock updated successfully");
			return Result<BeerStock>.Failure("Failed to update remaining stock");
		}

		public async Task<Result<List<BeerStock>>> GetAllStockByWholesaler(int wholesalerId)
		{
			var beerStocks = await _context.BeerStocks.Where(x => x.WholesalerId == wholesalerId).ToListAsync();
			if (beerStocks == null || beerStocks.Count == 0) return Result<List<BeerStock>>.Failure("No beer stocks found for this wholesaler");
			return Result<List<BeerStock>>.Success(beerStocks, "Beer stocks found successfully");
		}
	}
}
