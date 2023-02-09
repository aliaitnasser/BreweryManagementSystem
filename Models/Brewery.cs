using System;
using System.Linq;

namespace Models
{
	public class Brewery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Beer> Beers { get; set; }
	}
}
