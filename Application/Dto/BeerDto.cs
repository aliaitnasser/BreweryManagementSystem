﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
	public class BeerDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double AlcoholContent { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
