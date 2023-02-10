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
		
		public async Task<List<Beer>> GetAllBeers(int breweryId)
		{
			return await _context.Beers.Where(b => b.BreweryId == breweryId).ToListAsync();
		}
		public async Task AddBeer(Beer beer)
		{
			await _context.Beers.AddAsync(beer);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteBeer(int beerId, int breweryId)
		{
			var beer = await _context.Beers.FindAsync(beerId);
			
			if (beer.BreweryId == breweryId)
			{
				_context.Beers.Remove(beer);
				await _context.SaveChangesAsync();
			}
		}

        public async Task<List<Brewery>> GetAllBreweriesAsync()
        {
            return await _context.Breweries.ToListAsync();
        }
    }
}
