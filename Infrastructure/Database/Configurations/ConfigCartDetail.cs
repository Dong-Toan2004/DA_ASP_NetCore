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
	internal class ConfigCartDetail : IEntityTypeConfiguration<CartDetail>
	{
		public void Configure(EntityTypeBuilder<CartDetail> builder)
		{
			builder.ToTable("CartDetails");
			builder.HasKey(cd => cd.Id);
			builder.HasOne(cd => cd.Cart)
				.WithMany(c => c.CartDetails)
				.HasForeignKey(cd => cd.CartId);
			builder.HasOne(cd => cd.Product)
				.WithMany()
				.HasForeignKey(cd => cd.ProductId);
		}
	}
}
