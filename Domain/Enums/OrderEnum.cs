using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
	public enum OrderEnum
	{
		Pending = 0, //Đang chờ xử lý
		Delivering = 1, //Đang giao hàng
		Completed = 2, //Đã hoàn thành
		Cancelled = 3, //Đã hủy
	}
}
