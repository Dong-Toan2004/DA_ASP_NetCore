using Application.DataTransferObjects.CartDetail;
using Application.DataTransferObjects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace User.Controllers
{
	public class CartController : Controller
	{
		private readonly HttpClient _httpClient;
		public CartController(IHttpClientFactory factory)
		{
			_httpClient = factory.CreateClient("ApiClient");
		}

		[HttpGet]
		public async Task<IActionResult> Cart()
		{
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Login", "Authen");

			var response = await _httpClient.GetFromJsonAsync<ResponseObject<IEnumerable<CartDetailDto>>>(
					"api/CartDetail/GetAllCart");
			var data = response?.Data ?? Enumerable.Empty<CartDetailDto>();
			var cartQuantity = data.Sum(cd => cd.Quantity);
			ViewBag.CartQuantity = cartQuantity;
			return View(data);
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(Guid productId, int quantity)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Json(new
				{
					success = false,
					message = "Vui lòng đăng nhập"
				});
			}

			var dto = new CartDetailCreateDto
			{
				ProductId = productId,
				Quantity = quantity,
				IsActive = true,
				IsDeleted = false
			};

			var response =  await _httpClient.PostAsJsonAsync("api/CartDetail/AddToCart", dto);
			var result = await response.Content.ReadFromJsonAsync<ResponseObject<CartDetailDto>>();
			return Json(new
			{
				success = response.IsSuccessStatusCode,
				message = result?.Message
			});
		}
		[HttpGet]
		public async Task<IActionResult> GetCartQuantity()
		{
			if (!User.Identity.IsAuthenticated)
				return Json(new { quantity = 0 });

			var response = await _httpClient.GetFromJsonAsync<ResponseObject<IEnumerable<CartDetailDto>>>("api/CartDetail/GetAllCart");

			var quantity = response?.Data.Sum(x => x.Quantity) ?? 0;

			return Json(new { quantity });
		}

		[HttpPost]
		public async Task<IActionResult> Checkout()
		{
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Login", "Authen");
			var userId = Guid.Parse(User.FindFirst("UserId").Value);

			await _httpClient.PostAsync($"api/cart/checkout/{userId}", null);

			return RedirectToAction("Index");
		}

	}
}
