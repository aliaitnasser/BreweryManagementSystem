using Application.Core;
using Application.Dto;

using Models;

using System;
using System.Linq;

namespace Application.Repositories
{
	public interface IBrewerRepository
	{
		Task<Result<List<BeerDto>>> GetAllBeersByBrewery(int breweryId);
		Task<Result<CreateBeerDto>> AddBeer(CreateBeerDto beer);
		Task<Result<BeerDto>> DeleteBeer(int beerId, int breweryId);
	}
}
