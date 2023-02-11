using Application.Core;
using Models;

using System;
using System.Linq;

namespace Application.Repositories
{
	public interface IBrewerRepository
	{
		Task<Result<List<Beer>>> GetAllBeersByBrewery(int breweryId);
		Task<Result<Beer>> AddBeer(Beer beer);
		Task<Result<Beer>> DeleteBeer(int beerId, int breweryId);
	}
}
