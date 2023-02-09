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

		public Task<List<Beer>> GetAllBeers(int breweryId)
		{
			return _context.Beers.Where(b => b.BreweryId == breweryId).ToListAsync();
		}
		public async Task AddBeer(Beer beer)
		{
			_context.Beers.Add(beer);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteBeer(Beer beer)
		{
			var beerOnDatabase = await _context.Beers.FindAsync(beer);

			if (beerOnDatabase != null)
				_context.Beers.Remove(beer);
			await _context.SaveChangesAsync();
		}
	}
}
