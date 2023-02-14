using System;
using System.Linq;

namespace Application.Dto
{
	public class BeerStockDto
	{
		public int Id { get; set; }
		public int BeerId { get; set; }
		public int WholesalerId { get; set; }
		public int Quantity { get; set; }
	}
}
