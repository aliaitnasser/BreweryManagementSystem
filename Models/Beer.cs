using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models
{
	public class Beer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int BreweryId { get; set; }
		public Brewery Brewery { get; set; }
		public double AlcoholContent { get; set; }
		public int Quantity { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal Price { get; set; }
	}
}
