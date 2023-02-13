using Application.Core;
using Application.Repositories;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
	public class BreweriesController : BaseController
	{
		private readonly IBrewerRepository _breweryRepository;
		private readonly IMapper _mapper;

		public BreweriesController(IBrewerRepository breweryRepository, IMapper mapper)
		{
			_breweryRepository = breweryRepository;
			_mapper = mapper;
		}

		[HttpGet("{id}/beers")]
		public async Task<IActionResult> GetAllBeersByBrewery(int id)
		{
			var beers = await _breweryRepository.GetAllBeersByBrewery(id);

			//return HandleResult(beers);		
			if (beers.IsSuccess && beers.Value != null)
			{
				return Ok(beers.Value);
			}
			if (beers.IsSuccess && beers.Value == null)
			{
				return NotFound();
			}
			return BadRequest(beers.Error);
		}

		[HttpPost]
		public async Task<IActionResult> AddBeer(Beer beer)
		{
			var result = await _breweryRepository.AddBeer(beer);
			return HandleResult(result);
		}

		[HttpDelete("{breweryId}/beer/{beerId}")]
		public async Task<IActionResult> DeleteBeer(int beerId, int breweryId)
		{
			var result = await _breweryRepository.DeleteBeer(beerId, breweryId);
			return HandleResult(result);
		}
    }
}
