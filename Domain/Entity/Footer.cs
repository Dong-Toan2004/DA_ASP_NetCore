using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Footer
	{
		public Guid Id { get; set; }
		public string? Content { get; set; }
	}
}
