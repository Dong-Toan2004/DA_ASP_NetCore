using Application.DataTransferObjects.CartDetail;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CartDetailController : ControllerBase
	{
		public readonly ICartDetailRepository _cartDetailRepository;

		public CartDetailController(ICartDetailRepository cartDetailRepository)
		{
			_cartDetailRepository = cartDetailRepository;
		}
		[Authorize]
		[HttpGet]
		public async Task<ActionResult> GetAllCart()
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var cartExist = await _cartDetailRepository.GetCartById(Guid.Parse(userId));
			if (cartExist.Status != 200)
			{
				return NotFound(cartExist.Message);
			}
			if (cartExist.Data.UserId.ToString() != userId)
			{
				return Unauthorized("Người dùng không có quyền truy cập giỏ hàng này");
			}
			var result = await _cartDetailRepository.GetCartDetails(cartExist.Data.Id);
			return Ok(result);
		}
		[Authorize]
		[HttpPost]
		public async Task<ActionResult> AddToCart([FromBody]CartDetailCreateDto cartDetailCreateDto)
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var cartExist = await _cartDetailRepository.GetCartById(Guid.Parse(userId));
			if (cartExist.Status != 200)
			{
				return NotFound(cartExist.Message);
			}
			if (cartExist.Data.UserId.ToString() != userId)
			{
				return Unauthorized("Người dùng không có quyền truy cập giỏ hàng này");
			}
			var result = await _cartDetailRepository.AddToCart(cartExist.Data.Id, cartDetailCreateDto);
			if (result.Status != 200)
			{
				return BadRequest(result.Message);
			}
			return Ok(result);
		}
		[Authorize]
		[HttpDelete("{cartDetailId}")]
		public async Task<ActionResult> RemoveFromCart(Guid cartDetailId)
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var result = await _cartDetailRepository.RemoveFromCart(cartDetailId);
			return Ok(result);
		}
		[Authorize]
		[HttpPut("{cartDetailId}")]
		public async Task<ActionResult> UpdateCart(Guid cartDetailId, [FromBody] CartDetailUpdateDto cartDetailUpdateDto)
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var result = await _cartDetailRepository.UpdateCart(cartDetailId, cartDetailUpdateDto);
			return Ok(result);
		}
		[Authorize]
		[HttpDelete]
		public async Task<ActionResult> ClearCart()
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var cartExist = await _cartDetailRepository.GetCartById(Guid.Parse(userId));
			if (cartExist.Status != 200)
			{
				return NotFound(cartExist.Message);
			}
			if (cartExist.Data.UserId.ToString() != userId)
			{
				return Unauthorized("Người dùng không có quyền truy cập giỏ hàng này");
			}
			var result = await _cartDetailRepository.ClearCart(cartExist.Data.Id);
			return Ok(result);
		}
		[Authorize]
		[HttpPost("checkout")]
		public async Task<ActionResult> Checkout()
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var result = await _cartDetailRepository.Checkout(Guid.Parse(userId));
			return Ok(result);
		}
	}
}
