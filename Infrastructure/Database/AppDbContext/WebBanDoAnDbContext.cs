using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.AppDbContext
{
	public class WebBanDoAnDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
	{
		public WebBanDoAnDbContext(DbContextOptions<WebBanDoAnDbContext> options) : base(options)
		{
		}

		protected WebBanDoAnDbContext()
		{
		}

		#region DbSet Properties
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderDetail> OrderDetails { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<CartDetail> CartDetails { get; set; }
		public virtual DbSet<Payments> Payments { get; set; }
		public virtual DbSet<DeliveryInfo> DeliveryInfos { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }
		public virtual DbSet<SupportMessage> SupportMessages { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Discount> Discounts { get; set; }
		public virtual DbSet<Combo> Combos { get; set; }
		public virtual DbSet<ComboDetail> ComboDetails { get; set; }
		public virtual DbSet<Footer> Footers { get; set; }
		public virtual DbSet<Images> Images { get; set; }

		#endregion DbSet Properties

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=LAPTOP-JMN439Q3\\SQLEXPRESS02;Initial Catalog=WebBanDoAn;Integrated Security=True;TrustServerCertificate=true");
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(WebBanDoAnDbContext).Assembly);
		}
	}
}
