using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		public readonly IOrderRepository _orderRepository;

		public OrderController(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult> GetAllOrders()
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var result = await _orderRepository.GetAll(Guid.Parse(userId));
			return Ok(result);
		}

		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult> GetOrderById(Guid id)
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var result = await _orderRepository.GetById(id);
			return Ok(result);
		}
		[Authorize]
		[HttpGet]
		public async Task<ActionResult> GetGroupByOrder()
		{
			var userId = User.FindFirst("UserId")?.Value;
			if (userId == null)
			{
				return Unauthorized("Người dùng chưa đăng nhập");
			}
			var result = await _orderRepository.GetGroupByOrder(Guid.Parse(userId));
			return Ok(result);
		}
	}
}
