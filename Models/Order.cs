using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models
{
	public class Order
	{
		public int Id { get; set; }
		[Required]
		public int WholesalerId { get; set; }
		public Wholesaler Wholesaler { get; set; }
		[Required]
		public int BeerId { get; set; }
		public Beer Beer { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal OrderPrice { get; set; }
		
		
	}
}
