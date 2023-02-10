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
            return Ok(beerStocks);
        }

        [HttpPost]
        public async Task<IActionResult> AddSale(BeerStock beerStock)
        {
            await _wholesalerRepository.AddSale(beerStock);
            return Ok();
        }
    }
}
