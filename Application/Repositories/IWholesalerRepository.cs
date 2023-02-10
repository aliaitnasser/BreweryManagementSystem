using Models;

using System;
using System.Linq;

namespace Application.Repositories
{
	public interface IWholesalerRepository
	{
		Task AddSale(BeerStock wholesalerBeer);
		Task UpdateRemainingStock(int beerStockId ,int wholesalerId, int quantity);
		Task<List<BeerStock>> GetAllStockByWholesaler(int wholesalerId);
	}
}
