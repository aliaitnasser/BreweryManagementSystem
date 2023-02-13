using Application.Core;
using Models;

using System;
using System.Linq;

namespace Application.Repositories
{
	public interface IWholesalerRepository
	{
		Task<Result<BeerStock>> AddSale(BeerStock wholesalerBeer);
		Task<Result<BeerStock>> UpdateRemainingStock(int beerStockId ,int wholesalerId, int quantity);
		Task<Result<List<BeerStock>>> GetAllStockByWholesaler(int wholesalerId);
	}
}
