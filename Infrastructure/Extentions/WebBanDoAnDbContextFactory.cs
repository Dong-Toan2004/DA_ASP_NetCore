using Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extentions
{
	public class WebBanDoAnDbContextFactory : IDesignTimeDbContextFactory<WebBanDoAnDbContext>
	{
		public WebBanDoAnDbContext CreateDbContext(string[] args)
		{
			// Chuyển đến đường dẫn file có appsettings.json chạy dbcontext
			var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebAPI");
			var configuration = new ConfigurationBuilder()
				.SetBasePath(basePath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			var optionsBuilder = new DbContextOptionsBuilder<WebBanDoAnDbContext>();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

			return new WebBanDoAnDbContext(optionsBuilder.Options);
		}
	}
}
