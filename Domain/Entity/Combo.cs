using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Combo
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public string? ImageUrl { get; set; }
		public bool Status { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public virtual ICollection<ComboDetail>? ComboDetails { get; set; }
	}
}
