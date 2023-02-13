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
			if (wholesaler == null) return Result<Order>.Failure("The wholesaler must exist");

			var beer = await _context.Beers.FirstOrDefaultAsync(x => x.Id == order.BeerId);
			if (beer == null) return Result<Order>.Failure("Beer not found");

			var beerStock = await _context.BeerStocks.FirstOrDefaultAsync(x => x.BeerId == order.BeerId && x.WholesalerId == order.WholesalerId);
			if (beerStock == null) return Result<Order>.Failure("No stock for this beer");
			if (beerStock.Quantity < order.Quantity) return Result<Order>.Failure("The number of beers ordered cannot be greater than the wholesaler's stock");

			order.OrderPrice = beer.Price * order.Quantity;

			var discount = CalculateDiscount(order.Quantity, order.OrderPrice);
			order.OrderPrice -= discount;

			beerStock.Quantity -= order.Quantity;
			_context.BeerStocks.Update(beerStock);

			_context.Orders.Add(order);
			var result = await _context.SaveChangesAsync() > 0;
			if (result)
			{
				if(order.Quantity > 20) 
					return Result<Order>.Success(order, "Order added successfully. You have received a 20% discount");
				if(order.Quantity > 10) 
					return Result<Order>.Success(order, "Order added successfully. You have received a 10% discount");
				return Result<Order>.Success(order, "Order added successfully");
			}
			return Result<Order>.Failure("Failed to add order");
		}

		private static decimal CalculateDiscount(int quantity, decimal price)
		{
			decimal discount = 0;
			if (quantity > 20) return _ = price * 20 / 100;
			if (quantity > 10) return _ = price * 10 / 100;
			return discount;
		}
	}
}
