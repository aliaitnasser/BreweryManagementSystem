using Application.Core;
using Application.Dto;
using Application.Repositories;

using AutoMapper;

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

		[HttpGet("{id}/beers")]
		public async Task<IActionResult> GetAllBeersByBrewery(int id)
		{
			var beers = await _breweryRepository.GetAllBeersByBrewery(id);
			return HandleResultWithValue(beers);
		}

		[HttpPost]
		public async Task<IActionResult> AddBeer(CreateBeerDto beer)
		{
			var result = await _breweryRepository.AddBeer(beer);
			return HandleResultWithMessage(result);
		}

		[HttpDelete("{breweryId}/beer/{beerId}")]
		public async Task<IActionResult> DeleteBeer(int beerId, int breweryId)
		{
			var result = await _breweryRepository.DeleteBeer(beerId, breweryId);
			return HandleResultWithMessage(result);
		}
    }
}
