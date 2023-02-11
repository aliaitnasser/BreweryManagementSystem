using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models
{
	public class Beer
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int BreweryId { get; set; }
		public Brewery Brewery { get; set; }
		[Required]
		public double AlcoholContent { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal Price { get; set; }
	}
}
