using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.Product
{
	public class ProductSeachDto
	{
		public string? Name { get; set; }
		public Guid? CategoryId { get; set; }
		public bool? Status { get; set; }
		public Guid? DiscountId { get; set; }
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
		public string? ShortByPrice { get; set; }
		public string? SortOrder { get; set; } = "desc"; // Default to descending order
	}
}
