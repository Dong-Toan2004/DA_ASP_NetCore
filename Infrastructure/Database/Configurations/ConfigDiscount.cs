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
	internal class ConfigDiscount : IEntityTypeConfiguration<Discount>
	{
		public void Configure(EntityTypeBuilder<Discount> builder)
		{
			builder.ToTable("Discounts");
			builder.HasKey(d => d.Id);
		}
	}
}
