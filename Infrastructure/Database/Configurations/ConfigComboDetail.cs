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
	internal class ConfigComboDetail : IEntityTypeConfiguration<ComboDetail>
	{
		public void Configure(EntityTypeBuilder<ComboDetail> builder)
		{
			builder.ToTable("ComboDetails");
			builder.HasKey(cd => cd.Id);
			builder.HasOne(cd => cd.Combo)
				.WithMany(c => c.ComboDetails)
				.HasForeignKey(cd => cd.ComboId);
			builder.HasOne(cd => cd.Product)
				.WithMany(p => p.ComboDetails)
				.HasForeignKey(cd => cd.ProductId);
		}
	}
}
