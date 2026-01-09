using Application.DataTransferObjects.Order;
using Application.DataTransferObjects.OrderDetail;
using Application.DataTransferObjects.ViewModels;
using Application.Interfaces.IRepositories;
using Domain.Entity;
using Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		public readonly WebBanDoAnDbContext _context;

		public OrderRepository(WebBanDoAnDbContext context)
		{
			_context = context;
		}

		public async Task<ResponseObject<OrderDto>> DeleteOrder(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseObject<IEnumerable<OrderDto>>> GetAll(Guid userId)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user == null)
			{
				return new ResponseObject<IEnumerable<OrderDto>>
				{
					Status = 400,
					Message = "Người dùng không tồn tại",
					Data = null
				};
			}
			var orders = await _context.Orders
				.Where(o => o.UserId == userId)
				.Select(o => new OrderDto
				{
					Id = o.Id,
					UserId = o.UserId,
					OrderDate = o.OrderDate,
					TotalPrice = o.TotalPrice,
					Status = o.Status,
					PaymentID = o.PaymentID,
					DeliveryID = o.DeliveryID
				}).ToListAsync();
			return new ResponseObject<IEnumerable<OrderDto>>
			{
				Status = 200,
				Message = "Lấy danh sách đơn hàng thành công",
				Data = orders
			};
		}

		public async Task<ResponseObject<OrderDto>> GetById(Guid id)
		{
			var order = await _context.Orders.FindAsync(id);
			if (order == null)
			{
				return new ResponseObject<OrderDto>
				{
					Status = 404,
					Message = "Đơn hàng không tồn tại",
					Data = null
				};
			}
			return new ResponseObject<OrderDto>
			{
				Status = 200,
				Message = "Lấy đơn hàng thành công",
				Data = new OrderDto
				{
					Id = order.Id,
					UserId = order.UserId,
					OrderDate = order.OrderDate,
					TotalPrice = order.TotalPrice,
					Status = order.Status,
					PaymentID = order.PaymentID,
					DeliveryID = order.DeliveryID
				}
			};
		}

		public async Task<ResponseObject<IEnumerable<OrderWithDetailsDto>>> GetGroupByOrder(Guid userId)
		{
			var order = await _context.Orders.Where(o => o.UserId == userId)
				.Select(o => new OrderWithDetailsDto
				{
					Id = o.Id,
					UserId = o.UserId,
					OrderDate = o.OrderDate,
					TotalPrice = o.TotalPrice,
					Status = o.Status,
					PaymentID = o.PaymentID,
					DeliveryID = o.DeliveryID,
					OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
					{
						Id = od.Id,
						ProductId = od.ProductId,
						Quantity = od.Quantity,
						ProductName = od.Product.Name,
						ProductImage = od.Product.ImageUrl,
						ProductDescription = od.Product.Description
					}).ToList()
				}).ToListAsync();
			return new ResponseObject<IEnumerable<OrderWithDetailsDto>>
			{
				Status = 200,
				Message = "Lấy danh sách đơn hàng thành công",
				Data = order
			};
		}

		public async Task<ResponseObject<OrderDto>> UpdateOrder(Guid id, OrderUpdateDto orderUpdateDto)
		{
			throw new NotImplementedException();
		}
	}
}
