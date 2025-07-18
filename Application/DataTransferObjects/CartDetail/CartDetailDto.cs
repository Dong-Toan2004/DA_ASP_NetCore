using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.CartDetail
{
	public class CartDetailDto
	{
		public Guid Id { get; set; }
		public Guid CartId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public bool IsActive { get; set; } 
		public bool IsDeleted { get; set; } 
		public string? ProductName { get; set; }
		public string? ProductImageUrl { get; set; }
		public string? ProductDescription { get; set; }
	}
}
