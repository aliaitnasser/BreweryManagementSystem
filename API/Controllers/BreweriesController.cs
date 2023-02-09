using API.DTOs;
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

		[HttpGet("{id}")]
		public async Task<ActionResult<List<GetBeerDTO>>> GetAllBeersByBrewery(int id)
		{
			List<GetBeerDTO> beersDto = new List<GetBeerDTO>();

			var beers = await _breweryRepository.GetAllBeers(id);

			foreach (var beer in beers)
			{
				beersDto.Add(MapBeerToGetBeerDTO(beer));
			}

			return Ok(beersDto);
		}

		[HttpPost]
		public async Task<IActionResult> CreateBeer(CreateBeerDTO beerDTO)
		{
			var beer = new Beer
			{
				Name = beerDTO.Name,
				BreweryId = beerDTO.BreweryId,
				AlcoholContent = beerDTO.AlcoholContent,
				Quantity = beerDTO.Quantity,
				Price = beerDTO.Price
			};
			await _breweryRepository.AddBeer(beer);
			return Ok();
		}

		private GetBeerDTO MapBeerToGetBeerDTO(Beer beer)
		{
			return new GetBeerDTO
			{
				Id = beer.Id,
				Name = beer.Name,
				BreweryId = beer.BreweryId,
				AlcoholContent = beer.AlcoholContent,
				Quantity = beer.Quantity,
				Price = beer.Price
			};
		}
    }
}
