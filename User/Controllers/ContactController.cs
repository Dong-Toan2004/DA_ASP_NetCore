using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Contact()
		{
			return View();
		}
	}
}
