using MicroSample.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroSample.Domain.Configuration
{
  public class PostConfiguration : EntityTypeConfiguration<Post>
  {
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
      builder.ToTable("Post");
      builder.HasKey(x => x.Id);
    }
  }
}
