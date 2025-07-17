using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Images
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string? Url { get; set; }
		public string? AltText { get; set; }
		public virtual Product? Product { get; set; }
	}
}
