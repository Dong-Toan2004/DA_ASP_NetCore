using Application.DataTransferObjects.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
		public async Task<IActionResult> ProductDetail(ProductSeachDto productSeachDto)
		{
			var client = _httpClientFactory.CreateClient("ApiClient");
			productSeachDto ??= new ProductSeachDto();
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

		public async Task<IActionResult> SortProduct(ProductSeachDto productSeachDto)
		{
			var client = _httpClientFactory.CreateClient("ApiClient");
			productSeachDto ??= new ProductSeachDto();
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

		public IActionResult PromotionsProduct()
		{
			return View();
		}
	}
}
