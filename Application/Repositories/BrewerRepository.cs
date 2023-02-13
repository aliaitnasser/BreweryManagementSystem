using Application.Core;
using Application.Dto;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Models;

using Persistence;

using System;
using System.Linq;

namespace Application.Repositories
{
	public class BrewerRepository : IBrewerRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public BrewerRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		
		public async Task<Result<List<BeerDto>>> GetAllBeersByBrewery(int breweryId)
		{
			var breweryExists = await _context.Breweries.FindAsync(breweryId);
			if (breweryExists == null) return Result<List<BeerDto>>.Failure("Brewery does not exist");

			var beers = await _context.Beers.Where(b => b.BreweryId == breweryId).ToListAsync();
			if(beers == null || beers.Count == 0) return Result<List<BeerDto>>.Failure("No beers found for this brewery");

			var beersDto = _mapper.Map<List<Beer>, List<BeerDto>>(beers);
			return Result<List<BeerDto>>.Success(beersDto, "Beers found successfully");
		}

		public async Task<Result<CreateBeerDto>> AddBeer(CreateBeerDto beer)
		{
			var breweryExists = await _context.Breweries.FindAsync(beer.BreweryId);
			if (breweryExists == null) return Result<CreateBeerDto>.Failure("Brewery does not exist");

			var price = beer.Price;
			if (price <= 0) return Result<CreateBeerDto>.Failure("Price cannot be negative or zero");

			var quantity = beer.Quantity;
			if (quantity <= 0) return Result<CreateBeerDto>.Failure("Quantity cannot be negative or zero");

			var beerExists = await _context.Beers.AnyAsync(b => b.Name == beer.Name && b.BreweryId == beer.BreweryId);
			if (beerExists) return Result<CreateBeerDto>.Failure("Beer's name already exists");

			var beerToAdd = _mapper.Map<CreateBeerDto, Beer>(beer);
			await _context.Beers.AddAsync(beerToAdd);
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Result<CreateBeerDto>.Success(beer, "Beer added successfully");

			return Result<CreateBeerDto>.Failure("Failed to add beer");
		}

		public async Task<Result<BeerDto>> DeleteBeer(int beerId, int breweryId)
		{
			var beer = await _context.Beers.FindAsync(beerId);
			if (beer == null) return Result<BeerDto>.Failure("Beer does not exist");
			if(beer.BreweryId != breweryId) return Result<BeerDto>.Failure("Beer does not belong to this brewery");

			var beerDto = _mapper.Map<Beer, BeerDto>(beer);
			_context.Beers.Remove(beer);
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Result<BeerDto>.Success(beerDto, "Beer deleted successfully");
			return Result<BeerDto>.Failure("Failed to delete beer");
		}
    }
}
