using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models
{
	public class BeerStock
	{
		public int Id { get; set; }
		[Required]
		public int BeerId { get; set; }
		public Beer Beer { get; set; }
		[Required]
		public int WholesalerId { get; set; }
		public Wholesaler Wholesaler { get; set; }
		[Required]
		public int Quantity { get; set; }
	}
}
