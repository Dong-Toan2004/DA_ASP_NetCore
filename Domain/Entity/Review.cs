using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	public class Review
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public Guid ProductId { get; set; }
		public int Rating { get; set; } //Đánh giá từ 1 đến 5
		public string? Comment { get; set; } //Nhận xét của người dùng
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //Thời gian tạo đánh giá
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; //Thời gian cập nhật đánh giá
		public virtual ApplicationUser? User { get; set; }
		public virtual Product? Product { get; set; }
	}
}
