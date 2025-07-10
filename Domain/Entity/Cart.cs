using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Cart
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public virtual User? User { get; set; }
		public virtual ICollection<CartDetail>? CartDetails { get; set; }
	}
}
