using Application.Core;
using Application.Dto;

namespace Application.Repositories
{
	public interface IWholesalerRepository
	{
		Task<Result<BeerStockDto>> AddSaleAsync(BeerStockDto wholesalerBeer);
		Task<Result<BeerStockDto>> UpdateRemainingStockAsync(BeerStockDto beerStockDto);
		Task<Result<List<BeerStockDto>>> GetAllBeerStockByWholesaler(int id);
	}
}
