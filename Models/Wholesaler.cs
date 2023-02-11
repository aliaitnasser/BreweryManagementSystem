using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models
{
	public class Wholesaler
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public List<BeerStock> BeerStocks { get; set; }
		public List<Order> Orders { get; set; }
	}
}
