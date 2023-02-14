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
		Task<Result<BeerStockDto>> UpdateRemainingStock(int beerStockId ,int wholesalerId, int quantity);
	}
}
