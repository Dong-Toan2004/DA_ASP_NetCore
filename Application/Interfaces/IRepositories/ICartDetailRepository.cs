using Application.DataTransferObjects.CartDetail;
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
		Task<ResponseObject<CartDetailDto>> AddToCart(CartDetailCreateDto createDto);
		Task<ResponseObject<CartDetailDto>> UpdateCart(Guid id, CartDetailUpdateDto cartDetailUpdateDto);
		Task<ResponseObject<CartDetailDto>> RemoveFromCart(Guid productId);
		Task<ResponseObject<CartDetailDto>> ClearCart();
		Task<ResponseObject<IEnumerable<CartDetailDto>>> GetCartDetails();
		Task<ResponseObject<object>> Checkout();
	}
}
