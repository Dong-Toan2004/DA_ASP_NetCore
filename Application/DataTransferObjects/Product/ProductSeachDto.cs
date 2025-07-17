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
	}
}
