using Application.DataTransferObjects.Category;
using Application.DataTransferObjects.Product;
using Application.Interfaces.IRepositories;
using Infrastructure.Implements.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		public readonly ICategoryRepository _categoryRepository;
		public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpGet]
		public async Task<ActionResult<CategoryDto>> GetlAll()
		{
			var result = await _categoryRepository.GetAllCategories();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(Guid id)
		{
			var result = await _categoryRepository.GetCategoryById(id);
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
		{
			var result = await _categoryRepository.CreateCategory(categoryCreateDto);
			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryUpdateDto)
		{
			var result = await _categoryRepository.UpdateCategory(id, categoryUpdateDto);
			return Ok(result);
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteCategory(Guid id)
		{
			var result = await _categoryRepository.DeleteCategory(id);
			return Ok(result);
		}
	}
}
