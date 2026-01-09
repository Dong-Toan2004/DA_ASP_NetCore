using Application.DataTransferObjects.CartDetail;
using Application.DataTransferObjects.Product;
using Application.DataTransferObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
	public interface ICartDetailRepository
	{
		Task<ResponseObject<CartDetailDto>> AddToCart(Guid cartId,CartDetailCreateDto createDto);
		Task<ResponseObject<CartDetailDto>> UpdateCart(Guid cartDetailId, CartDetailUpdateDto cartDetailUpdateDto);
		Task<ResponseObject<CartDetailDto>> RemoveFromCart(Guid cartDetailId);
		Task<ResponseObject<CartDetailDto>> ClearCart(Guid cartId);
		Task<ResponseObject<IEnumerable<CartDetailDto>>> GetCartDetails(Guid cartId);
		Task<ResponseObject<object>> Checkout(Guid userId);
		Task<ResponseObject<CartDto>> GetCartById(Guid userId);
	}
}
