using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Product
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public string? ImageUrl { get; set; }
		public bool Status { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public Guid? CategoryId { get; set; }
		public Guid? DiscountId { get; set; }
		public virtual ICollection<CartDetail>? CartDetails { get; set; }
		public virtual Category? Category { get; set; }
		public virtual Discount? Discount { get; set; }
		public virtual ICollection<ComboDetail>? ComboDetails { get; set; }
		public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
		public virtual ICollection<Review>? Reviews { get; set; }

	}
}
