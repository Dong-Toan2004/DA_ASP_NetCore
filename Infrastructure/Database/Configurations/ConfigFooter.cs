﻿using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Configurations
{
	internal class ConfigFooter : IEntityTypeConfiguration<Footer>
	{	
		public void Configure(EntityTypeBuilder<Footer> builder)
		{
			builder.ToTable("Footers");
			builder.HasKey(f => f.Id);
		}
	}
}
