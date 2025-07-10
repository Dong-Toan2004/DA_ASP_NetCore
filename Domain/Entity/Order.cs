using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Order
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.UtcNow;
		public decimal TotalPrice { get; set; }
		public OrderEnum Status { get; set; } = OrderEnum.Pending;
		public Guid? PaymentID { get; set; }
		public Guid? DeliveryID { get; set; }
		public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
		public virtual Payments? Payment { get; set; }
		public virtual DeliveryInfo? Delivery { get; set; }

	}
}
