using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class User
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? PasswordHash { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public UserRole Role { get; set; } = UserRole.User;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public virtual Cart? Cart { get; set; }
		public virtual ICollection<Review>? Reviews { get; set; }
		public virtual ICollection<SupportMessage>? SupportMessages { get; set; }
	}
}
