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
	internal class ConfigCart : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable("Carts");
			builder.HasKey(c => c.Id);
			builder.HasOne(c => c.User)
				.WithOne(u => u.Cart)
				.HasForeignKey<Cart>(c => c.UserId);
		}
	}
}
