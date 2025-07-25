﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.Product
{
	public class ProductUpdateDto
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public string? ImageUrl { get; set; }
		public bool Status { get; set; } = false;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public Guid? CategoryId { get; set; }
		public Guid? DiscountId { get; set; }
	}
}
