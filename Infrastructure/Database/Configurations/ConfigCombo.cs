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
	internal class ConfigCombo : IEntityTypeConfiguration<Combo>
	{
		public void Configure(EntityTypeBuilder<Combo> builder)
		{
			builder.ToTable("Combos");
			builder.HasKey(c => c.Id);
		}
	}
}
