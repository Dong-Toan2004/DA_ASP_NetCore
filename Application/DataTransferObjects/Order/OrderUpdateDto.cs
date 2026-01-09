using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.Order
{
	public class OrderUpdateDto
	{
		public DateTime OrderDate { get; set; } = DateTime.UtcNow;
		public decimal TotalPrice { get; set; }
		public OrderEnum Status { get; set; } = OrderEnum.Pending;
		public Guid? PaymentID { get; set; }
		public Guid? DeliveryID { get; set; }
	}
}
