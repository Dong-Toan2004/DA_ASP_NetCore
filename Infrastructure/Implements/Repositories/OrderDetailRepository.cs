using Application.DataTransferObjects.Order;
using Application.DataTransferObjects.OrderDetail;
using Application.DataTransferObjects.ViewModels;
using Application.Interfaces.IRepositories;
using Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements.Repositories
{
	public class OrderDetailRepository : IOrderDetailRepository
	{
		public readonly WebBanDoAnDbContext _context;

		public OrderDetailRepository(WebBanDoAnDbContext context)
		{
			_context = context;
		}

		public async Task<ResponseObject<IEnumerable<OrderDetailDto>>> GetAll(Guid userId)
		{
			var orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
			var orderDetails = await _context.OrderDetails.Where (od => orders.Select(o => o.Id).Contains(od.OrderId))
				.Select(od => new OrderDetailDto
				{
					Id = od.Id,
					OrderId = od.OrderId,
					ProductId = od.ProductId,
					Quantity = od.Quantity,
					ProductName = od.Product.Name,
					ProductImage = od.Product.ImageUrl,
					ProductDescription = od.Product.Description,
				}).ToListAsync();

			return new ResponseObject<IEnumerable<OrderDetailDto>>
			{
				Status = 200,
				Message = "Order details retrieved successfully",
				Data = orderDetails
			};
		}

		public async Task<ResponseObject<OrderDetailDto>> GetById(Guid id)
		{
			var orderDetail = await _context.OrderDetails.FindAsync(id);
			if (orderDetail == null)
			{
				return new ResponseObject<OrderDetailDto>
				{
					Status = 404,
					Message = "Đơn hàng không tồn tại",
					Data = null
				};
			}
			return new ResponseObject<OrderDetailDto>
			{
				Status = 200,
				Message = "Lấy chi tiết đơn hàng thành công",
				Data = new OrderDetailDto
				{
					Id = orderDetail.Id,
					OrderId = orderDetail.OrderId,
					ProductId = orderDetail.ProductId,
					Quantity = orderDetail.Quantity,
					ProductName = orderDetail.Product.Name,
					ProductImage = orderDetail.Product.ImageUrl,
					ProductDescription = orderDetail.Product.Description,
					UnitPrice = orderDetail.UnitPrice
				}
			};
		}
	}
}
