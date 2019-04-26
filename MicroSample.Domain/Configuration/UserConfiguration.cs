using MicroSample.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroSample.Domain.Configuration
{
  public class UserConfiguration : EntityTypeConfiguration<User>
  {
    public override void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("User");
      builder.HasKey(x => x.Id);
    }
  }
}
