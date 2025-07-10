using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
	public enum DeliveryInfoEnum
	{
		delivering = 0, //Đang giao hàng
		delivered = 1, //Đã giao hàng
		cancelled = 2, //Đã hủy giao hàng
		pending = 3 //Đang chờ giao hàng
	}
}
