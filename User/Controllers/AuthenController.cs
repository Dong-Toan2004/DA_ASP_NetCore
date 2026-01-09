using Application.DataTransferObjects.Authen;
using Application.DataTransferObjects.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace User.Controllers
{
	public class AuthenController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public AuthenController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public IActionResult Login() => View(new LoginDto());

		[HttpPost]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			if (!ModelState.IsValid)
			{
				return View(loginDto);
			}

			var client = _httpClientFactory.CreateClient("ApiClient");
			var response = await client.PostAsJsonAsync(
				"api/Authen/Login",
				new { Email = loginDto.Email, Password = loginDto.Password }
			);

			if (!response.IsSuccessStatusCode)
			{
				ModelState.AddModelError("", "Đăng nhập thất bại");
				return View(loginDto);
			}

			var result = await response.Content
				.ReadFromJsonAsync<ResponseObject<object>>();

			var token = result?.Data?.ToString();
			if (string.IsNullOrEmpty(token))
			{
				ModelState.AddModelError("", "Không nhận được token");
				return View(loginDto);
			}

			// Giải mã JWT
			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			// ===== 1. LƯU JWT VÀO COOKIE RIÊNG (để gọi API) =====
			Response.Cookies.Append("token", token, new CookieOptions
			{
				HttpOnly = true,
				Secure = false,
				SameSite = SameSiteMode.Lax,
				Expires = jwtToken.ValidTo
			});

			// ===== 2. CLAIMS CHỈ DÙNG CHO UI =====
			var claims = new List<Claim>();

			var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
			if (!string.IsNullOrEmpty(userId))
				claims.Add(new Claim("UserId", userId));

			var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
			if (!string.IsNullOrEmpty(role))
				claims.Add(new Claim(ClaimTypes.Role, role));

			var name = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
			if (!string.IsNullOrEmpty(name))
				claims.Add(new Claim(ClaimTypes.Name, name));

			// ===== 3. COOKIE AUTHENTICATION (UI) =====
			var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

			var principal = new ClaimsPrincipal(identity);

			var props = new AuthenticationProperties
			{
				IsPersistent = true,
				ExpiresUtc = jwtToken.ValidTo
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal,props);

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			// Xóa cookie auth UI
			await HttpContext.SignOutAsync(
				CookieAuthenticationDefaults.AuthenticationScheme
			);

			// Xóa cookie JWT
			Response.Cookies.Delete("token");

			return RedirectToAction("Login");
		}
	}
}
