using Application.Core;
using Microsoft.EntityFrameworkCore;
using Models;

using Persistence;

namespace Application.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly DataContext _context;

		public OrderRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<Result<Order>> AddOrder(Order order)
		{
			var wholesaler = await _context.Wholesalers.FirstOrDefaultAsync(x => x.Id == order.WholesalerId);
			if (wholesaler == null) return Result<Order>.Failure("Wholesaler not found");

			var beer = await _context.Beers.FirstOrDefaultAsync(x => x.Id == order.BeerId);
			if (beer == null) return Result<Order>.Failure("Beer not found");

			var beerStock = await _context.BeerStocks.FirstOrDefaultAsync(x => x.BeerId == order.BeerId && x.WholesalerId == order.WholesalerId);
			if (beerStock == null) return Result<Order>.Failure("No stock for this beer");
			if (beerStock.Quantity < order.Quantity) return Result<Order>.Failure("Quantity must be less than or equal to the remaining stock");

			order.OrderPrice = beer.Price * order.Quantity;

			var discount = CalculateDiscount(order.Quantity, order.OrderPrice);

			order.OrderPrice -= discount;

			beerStock.Quantity -= order.Quantity;
			_context.BeerStocks.Update(beerStock);

			_context.Orders.Add(order);
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Result<Order>.Success(order);
			return Result<Order>.Failure("Failed to add order");
		}

		private static decimal CalculateDiscount(int quantity, decimal price)
		{
			decimal discount = 0;
			if (quantity > 10) return _ = price * 10 / 100;
			if (quantity > 20) return _ = price * 20 / 100;
			return discount;
		}
	}
}
