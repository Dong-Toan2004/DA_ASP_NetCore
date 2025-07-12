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
	internal class ConfigPayment : IEntityTypeConfiguration<Payments>
	{
		public void Configure(EntityTypeBuilder<Payments> builder)
		{
			builder.ToTable("Payments");
			builder.HasKey(p => p.Id);
			builder.HasOne(p => p.Order)
				.WithOne(o => o.Payment)
				.HasForeignKey<Payments>(p => p.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
