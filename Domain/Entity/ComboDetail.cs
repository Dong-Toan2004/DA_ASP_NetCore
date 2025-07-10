using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class ComboDetail
	{
		public Guid Id { get; set; }
		public Guid ComboId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public virtual Combo? Combo { get; set; }
		public virtual Product? Product { get; set; }
		
	}
}
