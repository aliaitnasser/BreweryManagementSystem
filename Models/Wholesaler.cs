using System;
using System.Linq;

namespace Models
{
	public class Wholesaler
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<WholesalerBeer> WholesalerBeers { get; set; }
	}
}
