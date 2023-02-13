using Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
