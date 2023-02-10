using System;
using System.Linq;

namespace Models
{
	public class Wholesaler
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<BeerStock> BeerStocks { get; set; }
		public List<Order> Orders { get; set; }
	}
}
