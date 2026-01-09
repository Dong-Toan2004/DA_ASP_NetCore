using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.OrderDetail
{
	public class OrderDetailDto
	{
		public Guid Id { get; set; }
		public Guid OrderId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public string ProductName { get; set; }= string.Empty;
		public string ProductImage { get; set; } = string.Empty;
		public string ProductDescription { get; set; } = string.Empty;
		public decimal UnitPrice { get; set; }
	}
}
