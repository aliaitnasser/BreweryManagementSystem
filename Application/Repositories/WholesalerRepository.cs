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
	public class WholesalerRepository : IWholesalerRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public WholesalerRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<Result<BeerStockDto>> AddSale(BeerStockDto beerStockDto)
		{
			var beer = await _context.Beers.FirstOrDefaultAsync(x => x.Id == beerStockDto.BeerId);
			if (beer == null) return Result<BeerStockDto>.Failure("Beer must exist");

			var wholesaler = await _context.Wholesalers.FirstOrDefaultAsync(x => x.Id == beerStockDto.WholesalerId);
			if (wholesaler == null) return Result<BeerStockDto>.Failure("Wholesaler must exist");

			if (beer.Quantity < beerStockDto.Quantity) return Result<BeerStockDto>.Failure("Quantity must be less than or equal to the remaining stock");

			beer.Quantity -= beerStockDto.Quantity;

			_context.Beers.Update(beer);

			var beerStockExist = _context.BeerStocks.FirstOrDefault(x => x.BeerId == beerStockDto.BeerId && x.WholesalerId == beerStockDto.WholesalerId);
			if (beerStockExist != null)
			{
				beerStockExist.Quantity += beerStockDto.Quantity;
				_context.BeerStocks.Update(beerStockExist);
			}
			else
			{
				var beerStock = _mapper.Map<BeerStock>(beerStockDto);
				_context.BeerStocks.Add(beerStock);
			}
			
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Result<BeerStockDto>.Success(beerStockDto, "Sale added successfully");
			return Result<BeerStockDto>.Failure("Failed to add sale");
		}

		public async Task<Result<BeerStockDto>> UpdateRemainingStock(int beerStockId ,int wholesalerId, int quantity)
		{
			var beerStock = await _context.BeerStocks.FirstOrDefaultAsync(x => x.Id == beerStockId);
			if (beerStock == null) return Result<BeerStockDto>.Failure("BeerStock does not exist");
			if(beerStock.WholesalerId != wholesalerId) return Result<BeerStockDto>.Failure("BeerStock does not belong to this wholesaler");
			
			beerStock.Quantity = quantity;
			_context.BeerStocks.Update(beerStock);
			var result = await _context.SaveChangesAsync() > 0;
			var beerStokDto = _mapper.Map<BeerStockDto>(beerStock);
			
			if (result) return Result<BeerStockDto>.Success(beerStokDto, "Remaining stock updated successfully");
			return Result<BeerStockDto>.Failure("Failed to update remaining stock");
		}
	}
}
