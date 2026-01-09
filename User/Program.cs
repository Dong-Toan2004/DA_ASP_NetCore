using Microsoft.AspNetCore.Authentication.Cookies;
using User.Extension_helper;

namespace User
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7028/") });
			builder.Services.AddControllersWithViews();
			//builder.WebHost.ConfigureKestrel(options =>
			//{
			//	options.ListenAnyIP(5000); // HTTP
			//	options.ListenAnyIP(5001, listenOptions =>
			//	{
			//		listenOptions.UseHttps(); // HTTPS
			//	});
			//});
			// Cookie authentication để giữ trạng thái đăng nhập
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/Authen/Login"; // Đường dẫn đến trang đăng nhập
					options.LogoutPath = "/Authen/Logout"; // Đường dẫn đến trang đăng xuất
					options.AccessDeniedPath = "/Authen/AccessDenied"; // Đường dẫn đến trang không có quyền truy cập
					options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian hết hạn cookie
				});
			// Thêm IHttpContextAccessor để truy cập HttpContext trong các dịch vụ khác
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddTransient<JwtTokenHandler>();
			builder.Services.AddScoped<AuthorizationClient>(); // Thêm AuthorizationClient để gọi API với token
															   // httpClient để gọi API
			builder.Services.AddHttpClient("ApiClient", client =>
			{
				client.BaseAddress = new Uri("https://localhost:7028/"); // Địa chỉ API
			}).AddHttpMessageHandler<JwtTokenHandler>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapStaticAssets();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}")
				.WithStaticAssets();

			app.Run();
		}
	}
}
