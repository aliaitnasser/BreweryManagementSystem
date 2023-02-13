using Application.Core;
using Application.Dto;

using Models;

using System;
using System.Linq;

namespace Application.Repositories
{
	public interface IWholesalerRepository
	{
		Task<Result<BeerStockDto>> AddSale(BeerStockDto wholesalerBeer);
		Task<Result<BeerStock>> UpdateRemainingStock(int beerStockId ,int wholesalerId, int quantity);
		Task<Result<List<BeerStockDto>>> GetAllStockByWholesaler(int wholesalerId);
	}
}
