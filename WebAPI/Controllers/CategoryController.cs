using Application.Interfaces.IRepositories;
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
		// GET: api/<CategoryController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<CategoryController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<CategoryController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<CategoryController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
