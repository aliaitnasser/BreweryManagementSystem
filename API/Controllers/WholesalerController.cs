using Application.Dto;
using Application.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class WholesalerController : BaseController
	{
		private readonly IWholesalerRepository _wholesalerRepository;

		public WholesalerController(IWholesalerRepository wholesalerRepository)
		{
			_wholesalerRepository = wholesalerRepository;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> AddSaleAsync(BeerStockDto beerStock)
		{
			var beerstock = await _wholesalerRepository.AddSaleAsync(beerStock);
			return HandleResultWithMessage(beerstock);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateRemainingStockAsync(BeerStockDto beerStockDto)
		{
			var updatedStock = await _wholesalerRepository.UpdateRemainingStockAsync(beerStockDto);
			return HandleResultWithMessage(updatedStock);
		}
	}
}
