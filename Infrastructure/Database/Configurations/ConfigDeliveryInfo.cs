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
	internal class ConfigDeliveryInfo : IEntityTypeConfiguration<DeliveryInfo>
	{
		public void Configure(EntityTypeBuilder<DeliveryInfo> builder)
		{
			builder.ToTable("DeliveryInfos");
			builder.HasKey(di => di.Id);
			builder.HasOne(di => di.Order)
				.WithOne(o => o.Delivery)
				.HasForeignKey<DeliveryInfo>(di => di.OrderId);
		}
	}
}
