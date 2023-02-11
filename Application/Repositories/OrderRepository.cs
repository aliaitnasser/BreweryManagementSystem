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

		public Task AddOrder(Order order)
		{
			var wholesaler = _context.Wholesalers.FirstOrDefault(x => x.Id == order.WholesalerId);
			if (wholesaler == null) throw new Exception("Wholesaler not found");

			var beer = _context.Beers.FirstOrDefault(x => x.Id == order.BeerId);
			if (beer == null) throw new Exception("Beer not found");

			var beerStock = _context.BeerStocks.FirstOrDefault(x => x.BeerId == order.BeerId && x.WholesalerId == order.WholesalerId);
			if (beerStock == null) throw new Exception("Beer stock not found");
			if (beerStock.Quantity < order.Quantity) throw new Exception("Not enough beer in stock");

			order.OrderPrice = beer.Price * order.Quantity;

			var discount = CalculateDiscount(order.Quantity, order.OrderPrice);

			order.OrderPrice -= discount;

			beerStock.Quantity -= order.Quantity;
			_context.BeerStocks.Update(beerStock);

			_context.Orders.Add(order);
			return _context.SaveChangesAsync();
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
