using Application.Core;
using Application.Dto;
using Application.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class BreweriesController : BaseController
	{
		private readonly IBrewerRepository _breweryRepository;

		public BreweriesController(IBrewerRepository breweryRepository)
		{
			_breweryRepository = breweryRepository;
		}

		[HttpGet("{breweryId}/beers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Result<List<BeerDto>>>> GetAllBeersByBreweryAsync(int breweryId)
		{
			var beers = await _breweryRepository.GetAllBeersByBrewery(breweryId);
			return HandleResultWithValue(beers);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> AddBeerAsync(CreateBeerDto beer)
		{
			var result = await _breweryRepository.AddBeer(beer);
			return HandleResultWithMessage(result);
		}

		[HttpDelete("{breweryId}/beer/{beerId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteBeerAsync(int beerId, int breweryId)
		{
			var result = await _breweryRepository.DeleteBeer(beerId, breweryId);
			return HandleResultWithMessage(result);
		}
	}
}
