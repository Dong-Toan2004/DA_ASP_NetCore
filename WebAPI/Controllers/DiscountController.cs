using Application.DataTransferObjects.Discount;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class DiscountController : ControllerBase
	{
		public readonly IDiscountRepository _repository;

		public DiscountController(IDiscountRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<ActionResult<DiscountDto>> GetAll([FromQuery] DiscountSearch discountSearch)
		{
			var discounts = await _repository.GetAll(discountSearch);
			return Ok(discounts);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<DiscountDto>> GetById(Guid id)
		{
			var discount = await _repository.GetById(id);
			return Ok(discount);
		}
		[HttpPost]
		public async Task<ActionResult> CreateDiscount([FromBody] DiscountCreateDto discountCreateDto)
		{
			var result = await _repository.CreateDiscount(discountCreateDto);
			return Ok(result);
		}
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateDiscount(Guid id, [FromBody] DiscountUpdateDto discountUpdateDto)
		{
			var result = await _repository.UpdateDiscount(id, discountUpdateDto);
			return Ok(result);
		}
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteDiscount(Guid id)
		{
			var result = await _repository.DeleteDiscount(id);
			return Ok(result);
		}
	}
}
