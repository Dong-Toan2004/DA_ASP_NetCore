using Microsoft.AspNetCore.Mvc;
using User.Extension_helper;

namespace User.Controllers
{
	public class ProductController : BaseController
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public ProductController(IHttpClientFactory factory):base(factory)
		{
			_httpClientFactory = factory;
		}
		public IActionResult ProductDetail()
		{
			return View();
		}
	}
}
