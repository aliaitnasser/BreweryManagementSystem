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

        [HttpGet("{wholesalerId}/stocks")]
        public async Task<IActionResult> GetAllStockByWholesaler(int wholesalerId)
        {
            var beerStocks = await _wholesalerRepository.GetAllStockByWholesaler(wholesalerId);
            return HandleResult(beerStocks);
        }

        [HttpPost]
        public async Task<IActionResult> AddSale(BeerStock beerStock)
        {
            var beerstock = await _wholesalerRepository.AddSale(beerStock);
            return HandleResult(beerstock);
        }

        [HttpPut("{wholesalerId}/stocks/{beerStockId}")]
        public async Task<IActionResult> UpdateRemainingStock(int beerStockId ,int wholesalerId,[FromBody] int quantity)
        {
            var updatedStock = await _wholesalerRepository.UpdateRemainingStock(beerStockId ,wholesalerId, quantity);
            return HandleResult(updatedStock);
        }
    }
}
