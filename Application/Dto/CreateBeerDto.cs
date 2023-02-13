using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
	public class CreateBeerDto
	{
		public string Name { get; set; }
		public int BreweryId { get; set; }
		public double AlcoholContent { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
