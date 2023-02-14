using Application.Dto;
using Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;

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
        public async Task<IActionResult> AddSale(BeerStockDto beerStock)
        {
            var beerstock = await _wholesalerRepository.AddSale(beerStock);
            return HandleResultWithMessage(beerstock);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRemainingStock(BeerStockDto beerStockDto)
        {
            var updatedStock = await _wholesalerRepository.UpdateRemainingStock(beerStockDto);
            return HandleResultWithMessage(updatedStock);
        }

        [HttpGet("{wholesalerId}")]
        public async Task<IActionResult> GetWholesalerStock(int wholesalerId)
        {
			var stock = await _wholesalerRepository.GetAllBeerStockByWholesaler(wholesalerId);
			return HandleResultWithValue(stock);
		}
    }
}
