using Application.DataTransferObjects.OrderDetail;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.Order
{
	public class OrderWithDetailsDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.UtcNow;
		public decimal TotalPrice { get; set; }
		public OrderEnum Status { get; set; } = OrderEnum.Pending;
		public Guid? PaymentID { get; set; }
		public Guid? DeliveryID { get; set; }
		public List<OrderDetailDto> OrderDetails { get; set; }
	}
}
