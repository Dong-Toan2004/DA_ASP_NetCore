using Application.DataTransferObjects.CartDetail;
using Application.DataTransferObjects.Product;
using Application.DataTransferObjects.ViewModels;
using Application.Interfaces.IRepositories;
using Domain.Entity;
using Domain.Enums;
using Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements.Repositories
{
	public class CartDetailRepository : ICartDetailRepository
	{
		public readonly WebBanDoAnDbContext _context;
		public CartDetailRepository(WebBanDoAnDbContext context)
		{
			_context = context;
		}

		public async Task<ResponseObject<CartDetailDto>> AddToCart(Guid cartId, CartDetailCreateDto createDto)
		{
			var product = await _context.Products
				.FirstOrDefaultAsync(p => p.Id == createDto.ProductId);

			if (product == null)
			{
				return new ResponseObject<CartDetailDto>
				{
					Status = 404,
					Message = "Sản phẩm không tồn tại",
					Data = null
				};
			}

			if (createDto.Quantity <= 0)
			{
				createDto.Quantity = 1;
			}

			var cartDetailExists = await _context.CartDetails
				.FirstOrDefaultAsync(cd => cd.CartId == cartId && cd.ProductId == createDto.ProductId);

			if (cartDetailExists != null)
			{
				cartDetailExists.Quantity += createDto.Quantity;
				cartDetailExists.Price = product.Price * cartDetailExists.Quantity;

				_context.CartDetails.Update(cartDetailExists);
				await _context.SaveChangesAsync();

				return new ResponseObject<CartDetailDto>
				{
					Status = 200,
					Message = "Cập nhật số lượng sản phẩm trong giỏ hàng",
					Data = new CartDetailDto
					{
						Id = cartDetailExists.Id,
						CartId = cartDetailExists.CartId,
						ProductId = cartDetailExists.ProductId,
						Quantity = cartDetailExists.Quantity,
						Price = cartDetailExists.Price,
						IsActive = cartDetailExists.IsActive,
						IsDeleted = cartDetailExists.IsDeleted
					}
				};
			}

			var cartDetail = new CartDetail
			{
				Id = Guid.NewGuid(),
				CartId = cartId,
				ProductId = createDto.ProductId,
				Quantity = createDto.Quantity,
				Price = product.Price * createDto.Quantity,
				IsActive = createDto.IsActive,
				IsDeleted = createDto.IsDeleted
			};

			await _context.CartDetails.AddAsync(cartDetail);
			await _context.SaveChangesAsync();

			return new ResponseObject<CartDetailDto>
			{
				Status = 200,
				Message = "Thêm sản phẩm vào giỏ hàng thành công",
				Data = new CartDetailDto
				{
					Id = cartDetail.Id,
					CartId = cartDetail.CartId,
					ProductId = cartDetail.ProductId,
					Quantity = cartDetail.Quantity,
					Price = cartDetail.Price,
					IsActive = cartDetail.IsActive,
					IsDeleted = cartDetail.IsDeleted
				}
			};
		}


		public async Task<ResponseObject<object>> Checkout(Guid userId)
		{
			var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
			if (cart == null)
			{
				return new ResponseObject<object>
				{
					Status = 404,
					Message = "Giỏ hàng không tồn tại",
					Data = null
				};
			}
			var cartDetails = await _context.CartDetails
				.Where(cd => cd.CartId == cart.Id && cd.IsActive)
				.ToListAsync();
			if (!cartDetails.Any())
			{
				return new ResponseObject<object>
				{
					Status = 404,
					Message = "Giỏ hàng không có sản phẩm để thanh toán",
					Data = null
				};
			}

			var totalAmount = cartDetails.Sum(cd => cd.Price);

			var order = new Order
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				OrderDate = DateTime.UtcNow,
				TotalPrice = totalAmount,
				Status = OrderEnum.Pending,
				OrderDetails = new List<OrderDetail>()
			};

			foreach (var detail in cartDetails)
			{
				var orderDetail = new OrderDetail
				{
					Id = Guid.NewGuid(),
					OrderId = order.Id,
					ProductId = detail.ProductId,
					Quantity = detail.Quantity,
					UnitPrice = detail.Price
				};
				order.OrderDetails.Add(orderDetail);
			}
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				await _context.Orders.AddAsync(order);
				_context.CartDetails.RemoveRange(cartDetails);
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();
				return new ResponseObject<object>
				{
					Status = 200,
					Message = "Chuyển sang hóa đơn thành công",
					Data = new
					{
						OrderId = order.Id,
						UserId = order.UserId,
						TotalPrice = order.TotalPrice,
						OrderDate = order.OrderDate,
						Status = order.Status
					}
				};
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return new ResponseObject<object>
				{
					Status = 500,
					Message = "Lỗi khi chuyển sang hóa đơn: " + ex.Message,
					Data = null
				};
			}
		}

		public async Task<ResponseObject<CartDetailDto>> ClearCart(Guid cartId)
		{
			var cartDetails = await _context.CartDetails
				.Where(cd => cd.CartId == cartId && !cd.IsDeleted).ToListAsync();
			if (!cartDetails.Any())
			{
				return new ResponseObject<CartDetailDto>
				{
					Status = 404,
					Message = "Giỏ hàng không có sản phẩm để xóa",
					Data = null
				};
			}
			foreach (var detail in cartDetails)
			{
				_context.CartDetails.Remove(detail);
			}
			await _context.SaveChangesAsync();
			return new ResponseObject<CartDetailDto>
			{
				Status = 200,
				Message = "Đã xóa tất cả sản phẩm trong giỏ hàng",
				Data = null
			};
		}

		public async Task<ResponseObject<CartDto>> GetCartById(Guid userId)
		{
			var carts = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
			return new ResponseObject<CartDto>
			{
				Status = 200,
				Message = "Lấy giỏ hàng thành công",
				Data = new CartDto
				{
					Id = carts.Id,
					UserId = carts.UserId,
				}
			};
		}

		public async Task<ResponseObject<IEnumerable<CartDetailDto>>> GetCartDetails(Guid cartId)
		{
			var cartDetails = await _context.CartDetails
				.Where(cd => cd.CartId == cartId)
				.ToListAsync();
			if (!cartDetails.Any())
			{
				return new ResponseObject<IEnumerable<CartDetailDto>>
				{
					Status = 404,
					Message = "Giỏ hàng không có sản phẩm",
					Data = null
				};
			}
			var productIds = cartDetails.Select(cd => cd.ProductId).Distinct();
			var productInformation = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
			var cartDetailDtos = cartDetails.Select(cd =>
			{
				var product = productInformation.FirstOrDefault(p => p.Id == cd.ProductId);
				return new CartDetailDto
				{
					Id = cd.Id,
					CartId = cd.CartId,
					ProductId = cd.ProductId,
					ProductName = product?.Name,
					ProductImageUrl = product?.ImageUrl,
					ProductDescription = product?.Description,
					Quantity = cd.Quantity,
					Price = cd.Price,
					IsActive = cd.IsActive,
					IsDeleted = cd.IsDeleted
				};
			});
			return new ResponseObject<IEnumerable<CartDetailDto>>
			{
				Status = 200,
				Message = "Lấy danh sách sản phẩm trong giỏ hàng thành công",
				Data = cartDetailDtos
			};
		}

		public async Task<ResponseObject<CartDetailDto>> RemoveFromCart(Guid cartDetailId)
		{
			var cartDetail = await _context.CartDetails
				.FirstOrDefaultAsync(cd => cd.Id == cartDetailId);
			if (cartDetail == null)
			{
				return new ResponseObject<CartDetailDto>
				{
					Status = 404,
					Message = "Sản phẩm không tồn tại trong giỏ hàng",
					Data = null
				};
			}
			_context.CartDetails.Remove(cartDetail);
			await _context.SaveChangesAsync();
			return new ResponseObject<CartDetailDto>
			{
				Status = 200,
				Message = "Sản phẩm đã được xóa khỏi giỏ hàng",
				Data = null
			};
		}

		public async Task<ResponseObject<CartDetailDto>> UpdateCart(Guid cartDetailId, CartDetailUpdateDto cartDetailUpdateDto)
		{
			var cartDetail = await _context.CartDetails
				.FirstOrDefaultAsync(cd => cd.Id == cartDetailId);
			if (cartDetail == null)
			{
				return new ResponseObject<CartDetailDto>
				{
					Status = 404,
					Message = "Sản phẩm không tồn tại trong giỏ hàng",
					Data = null
				};
			}
			if (cartDetailUpdateDto.Quantity <= 0)
			{
				return new ResponseObject<CartDetailDto>
				{
					Status = 400,
					Message = "Số lượng sản phẩm phải lớn hơn 0",
					Data = null
				};
			}
			var product = await _context.Products
				.FirstOrDefaultAsync(p => p.Id == cartDetail.ProductId);
			if (product == null)
			{
				return new ResponseObject<CartDetailDto>
				{
					Status = 404,
					Message = "Sản phẩm không tồn tại",
					Data = null
				};
			}
			cartDetail.Quantity = cartDetailUpdateDto.Quantity;
			cartDetail.Price = product.Price * cartDetailUpdateDto.Quantity;
			cartDetail.IsActive = cartDetailUpdateDto.IsActive;
			cartDetail.IsDeleted = cartDetailUpdateDto.IsDeleted;
			_context.CartDetails.Update(cartDetail);
			await _context.SaveChangesAsync();
			return new ResponseObject<CartDetailDto>
			{
				Status = 200,
				Message = "Cập nhật sản phẩm trong giỏ hàng thành công",
				Data = new CartDetailDto
				{
					Id = cartDetail.Id,
					CartId = cartDetail.CartId,
					ProductId = cartDetail.ProductId,
					Quantity = cartDetail.Quantity,
					Price = cartDetail.Price,
					IsActive = cartDetail.IsActive,
					IsDeleted = cartDetail.IsDeleted
				}
			};
		}

	}
}
