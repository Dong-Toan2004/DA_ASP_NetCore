using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public string? Name { get; set; }
		public string? Address { get; set; }
		public UserRole Role { get; set; } = UserRole.User;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public virtual Cart? Cart { get; set; }
		public virtual ICollection<Order>? Orders { get; set; }
		public virtual ICollection<Review>? Reviews { get; set; }
		public virtual ICollection<SupportMessage>? SupportMessages { get; set; }
	}
}
