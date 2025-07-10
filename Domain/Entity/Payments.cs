using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Payments
	{
		public Guid Id { get; set; }
		public Guid OrderId { get; set; }
		public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
		public decimal Amount { get; set; }
		public string PaymentMethod { get; set; } = string.Empty;
		public PaymentEnum Status { get; set; } = PaymentEnum.Pending; 
		public virtual Order? Order { get; set; }
	}
}
