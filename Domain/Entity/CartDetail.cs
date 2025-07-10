using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class CartDetail
	{
		public Guid Id { get; set; }
		public Guid CartId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public virtual Cart? Cart { get; set; }
		public virtual Product? Product { get; set; }
	}
}
