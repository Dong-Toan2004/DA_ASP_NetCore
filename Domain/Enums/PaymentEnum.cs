using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
	public enum PaymentEnum
	{
		Pending = 0, // Đang chờ xử lý
		Completed = 1, // Đã hoàn thành
		Failed = 2, // Đã thất bại
		Refunded = 3 // Đã hoàn tiền
	}
}
