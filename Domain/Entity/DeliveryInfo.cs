using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class DeliveryInfo
	{
		public Guid Id { get; set; }
		public Guid OrderId { get; set; }
		public string? ReceiverName { get; set; } //Tên người nhận
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? City { get; set; } //Thành phố
		public string? District { get; set; } //Quận
		public string? Ward { get; set; } //Phường
		public DeliveryInfoEnum Status { get; set; } = DeliveryInfoEnum.pending; //Trạng thái chờ giao hàng
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? DeliveredAt { get; set; } //Thời gian giao hàng
		public virtual Order? Order { get; set; }
	}
}
