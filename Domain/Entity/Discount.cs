﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Discount
	{
		public Guid Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Code { get; set; }
		public decimal Percentage { get; set; }
		public bool Status { get; set; } = false;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public virtual ICollection<Product>? Products { get; set; }
	}
}
