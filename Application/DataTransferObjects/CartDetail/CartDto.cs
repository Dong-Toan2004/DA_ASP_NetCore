using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.CartDetail
{
	public class CartDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
	}
}
