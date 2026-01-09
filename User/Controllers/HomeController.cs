using Application.DataTransferObjects.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using User.Models;

namespace User.Controllers
{
	public class HomeController : BaseController 
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory _httpClientFactory;
		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
		{
			_logger = logger;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index(ProductSeachDto productSeachDto)
		{
			var client = _httpClientFactory.CreateClient("ApiClient");
			if (productSeachDto == null)
			{
				return View(new List<ProductDto>());
			}

			var queryParams = new Dictionary<string, string>
			{

			};

			if (!string.IsNullOrEmpty(productSeachDto.Name))
			{
				queryParams.Add("name", productSeachDto.Name);
			}
			if (productSeachDto.CategoryId.HasValue)
			{
				queryParams.Add("categoryId", productSeachDto.CategoryId.Value.ToString());
			}
			if (productSeachDto.Status.HasValue)
			{
				queryParams.Add("status", productSeachDto.Status.Value.ToString());
			}
			if (productSeachDto.DiscountId.HasValue)
			{
				queryParams.Add("discountId", productSeachDto.DiscountId.Value.ToString());
			}
			if (productSeachDto.MinPrice.HasValue)
			{
				queryParams.Add("minPrice", productSeachDto.MinPrice.Value.ToString());
			}
			if (productSeachDto.MaxPrice.HasValue)
			{
				queryParams.Add("maxPrice", productSeachDto.MaxPrice.Value.ToString());
			}
			if (!string.IsNullOrEmpty(productSeachDto.ShortByPrice))
			{
				queryParams.Add("shortByPrice", productSeachDto.ShortByPrice);
			}

			var result = QueryHelpers.AddQueryString("api/Product/GetAll", queryParams);
			var response = await client.GetFromJsonAsync<IEnumerable<ProductDto>>(result);

			return View(response);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
