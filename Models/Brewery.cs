using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models
{
	public class Brewery
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public List<Beer> Beers { get; set; }
	}
}
