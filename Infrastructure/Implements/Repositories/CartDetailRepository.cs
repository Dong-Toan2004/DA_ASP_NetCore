using Application.DataTransferObjects.CartDetail;
using Application.DataTransferObjects.ViewModels;
using Application.Interfaces.IRepositories;
using Infrastructure.Database.AppDbContext;
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

		public async Task<ResponseObject<CartDetailDto>> AddToCart(CartDetailCreateDto createDto)
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseObject<object>> Checkout()
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseObject<CartDetailDto>> ClearCart()
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseObject<IEnumerable<CartDetailDto>>> GetCartDetails()
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseObject<CartDetailDto>> RemoveFromCart(Guid productId)
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseObject<CartDetailDto>> UpdateCart(Guid id, CartDetailUpdateDto cartDetailUpdateDto)
		{
			throw new NotImplementedException();
		}
	}
}
