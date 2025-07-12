using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Configurations
{
	internal class ConfigOrder : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders");
			builder.HasKey(o => o.Id);
			builder.HasOne(o => o.User)
				.WithMany(u => u.Orders)
				.HasForeignKey(o => o.UserId);
			builder.HasOne(o => o.Payment)
				.WithOne(p => p.Order)
				.HasForeignKey<Order>(p => p.PaymentID);
			builder.HasOne(o => o.Delivery)
				.WithOne(d => d.Order)
				.HasForeignKey<Order>(d => d.DeliveryID);
		}
	}
}
