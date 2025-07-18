using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.CartDetail
{
	public class CartDetailUpdateDto
	{
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal TotalPrice { get; set; }
		public string? Description { get; set; }
		public bool IsActive { get; set; } 
		public bool IsDeleted { get; set; }
	}
}
