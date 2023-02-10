using Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class BreweriesController : BaseController
    {
        private readonly IBrewerRepository _breweryRepository;
        public BreweriesController(IBrewerRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }
		
		[HttpGet]
		public async Task<ActionResult<List<Brewery>>> GetAllBreweries()
		{
			var breweries = await _breweryRepository.GetAllBreweriesAsync();
			return Ok(breweries);
		}

		[HttpGet("{id}/beers")]
		public async Task<ActionResult<List<Beer>>> GetAllBeersByBrewery(int id)
		{
			var beers = await _breweryRepository.GetAllBeers(id);
			return Ok(beers);
		}

		[HttpPost]
		public async Task<IActionResult> AddBeer(Beer beer)
		{
			await _breweryRepository.AddBeer(beer);
			return Ok();
		}

		[HttpDelete("{breweryId}/beer/{beerId}")]
		public async Task<IActionResult> DeleteBeer(int beerId, int breweryId)
		{
			await _breweryRepository.DeleteBeer(beerId, breweryId);
			return Ok();
		}
    }
}
