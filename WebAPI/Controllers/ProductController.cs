using Application.DataTransferObjects.Product;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepository;

		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		
		[HttpPost]
		public async Task<ActionResult> CreateProduct([FromBody] ProductCreatDto product)
		{
			var result = await _productRepository.CreateProduct(product);
			return Ok(result);
		}
		[HttpGet]
		public async Task<ActionResult<ProductDto>> GetlAll([FromQuery] ProductSeachDto productSeach)
		{
			var result = await _productRepository.GetAllProducts(productSeach);
			return Ok(result);
		}
		[HttpPut]
		public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDto product)
		{
			var result = await _productRepository.UpdateProduct(id, product);
			return Ok(result);
		}
		[HttpDelete]
		public async Task<ActionResult> DeleteProduct(Guid id)
		{
			var result = await _productRepository.DeleteProduct(id);
			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto>> GetById(Guid id)
		{
			var result = await _productRepository.ProductById(id);
			return Ok(result);
		}
	}
}
