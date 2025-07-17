using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Database.Configurations
{
	public class ConfigImage : IEntityTypeConfiguration<Images>
	{
		public void Configure(EntityTypeBuilder<Images> builder)
		{
			builder.ToTable("Images");
			builder.HasKey(i => i.Id);
			builder.HasOne(i => i.Product)
				.WithMany(p => p.Images)
				.HasForeignKey(i => i.ProductId);
		}
	}
}
