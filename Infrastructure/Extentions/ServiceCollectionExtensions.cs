using Application.Interfaces.IRepositories;
using Infrastructure.Database.AppDbContext;
using Infrastructure.Implements.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extentions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<WebBanDoAnDbContext>(option =>
				option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IDiscountRepository, DiscountRepository>();
			services.AddScoped<ICartDetailRepository, CartDetailRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
			return services;
		}
	}
}
