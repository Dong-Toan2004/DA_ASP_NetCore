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
	internal class ConfigSupportMessage : IEntityTypeConfiguration<SupportMessage>
	{
		public void Configure(EntityTypeBuilder<SupportMessage> builder)
		{
			builder.ToTable("SupportMessages");
			builder.HasKey(sm => sm.Id);
			builder.HasOne(sm => sm.User)
				.WithMany(u => u.SupportMessages)
				.HasForeignKey(sm => sm.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
