using Application.Core;
using Microsoft.EntityFrameworkCore;

using Models;

using Persistence;

using System;
using System.Linq;

namespace Application.Repositories
{
	public class BrewerRepository : IBrewerRepository
	{
		private readonly DataContext _context;

		public BrewerRepository(DataContext context)
		{
			_context = context;
		}
		
		public async Task<Result<List<Beer>>> GetAllBeersByBrewery(int breweryId)
		{
			var breweryExists = await _context.Breweries.FindAsync(breweryId);
			if (breweryExists == null) return Result<List<Beer>>.Failure("Brewery does not exist");

			var beers = await _context.Beers.Where(b => b.BreweryId == breweryId).ToListAsync();
			if(beers == null || beers.Count == 0) return Result<List<Beer>>.Failure("No beers found for this brewery");

			return Result<List<Beer>>.Success(beers);
		}
		public async Task<Result<Beer>> AddBeer(Beer beer)
		{
			var breweryExists = await _context.Breweries.FindAsync(beer.BreweryId);
			if (breweryExists == null) return Result<Beer>.Failure("Brewery does not exist");

			var price = beer.Price;
			if (price <= 0) return Result<Beer>.Failure("Price cannot be negative or zero");

			var quantity = beer.Quantity;
			if (quantity <= 0) return Result<Beer>.Failure("Quantity cannot be negative or zero");

			var beerExists = await _context.Beers.AnyAsync(b => b.Name == beer.Name && b.BreweryId == beer.BreweryId);
			if (beerExists) return Result<Beer>.Failure("Beer's name already exists");

			await _context.Beers.AddAsync(beer);
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Result<Beer>.Success(beer);

			return Result<Beer>.Failure("Failed to add beer");
		}

		public async Task<Result<Beer>> DeleteBeer(int beerId, int breweryId)
		{
			var beer = await _context.Beers.FindAsync(beerId);
			if (beer == null) return Result<Beer>.Failure("Beer does not exist");
			if(beer.BreweryId != breweryId) return Result<Beer>.Failure("Beer does not belong to this brewery");

			_context.Beers.Remove(beer);
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Result<Beer>.Success(beer);
			return Result<Beer>.Failure("Failed to delete beer");
		}
    }
}
