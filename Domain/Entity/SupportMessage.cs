using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class SupportMessage
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public UserRole SenderRole { get; set; }
		public string? Message { get; set; }
		public DateTime SentAt { get; set; } = DateTime.UtcNow;
		public virtual User? User { get; set; } 
	}
}
