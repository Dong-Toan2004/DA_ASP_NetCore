using Application.DataTransferObjects.CartDetail;
using Application.DataTransferObjects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace User.Controllers
{
	public class BaseController : Controller
	{
		protected readonly IHttpClientFactory _httpClientFactory;

		public BaseController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (User.Identity.IsAuthenticated)
			{
				try
				{
					var client = _httpClientFactory.CreateClient("ApiClient");
					var response = await client.GetFromJsonAsync<ResponseObject<IEnumerable<CartDetailDto>>>("api/CartDetail/GetAllCart");
					ViewBag.CartQuantity = response?.Data.Sum(cd => cd.Quantity) ?? 0;
				}
				catch
				{
					ViewBag.CartQuantity = 0;
				}
			}
			else
			{
				ViewBag.CartQuantity = 0;
			}

			await base.OnActionExecutionAsync(context, next);
		}
	}
}
