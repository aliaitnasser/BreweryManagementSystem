using Models;

using System;
using System.Linq;

namespace Application.Repositories
{
	public interface IWholesalerRepository
	{
		Task AddSale(BeerStock wholesalerBeer);
		Task UpdateRemainingStock(int wholesalerBeerId, int stock);
		Task<List<BeerStock>> GetAllStockByWholesaler(int wholesalerId);
	}
}
