using Forum.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content).HasColumnName("Content").IsRequired().HasMaxLength(8000);
            builder.Property(c => c.Active).HasColumnName("Active").IsRequired();
            builder.Property(c => c.InsertDate).HasColumnName("InsertDate");
            builder.Property(c => c.OwnerUserId).HasColumnName("OwnerUserId").IsRequired();
            builder.Property(c => c.PostId).HasColumnName("PostId").IsRequired();

            builder.HasOne<User>(ec => ec.OwnerUser)
                .WithMany(c => c.Comments)
                .HasForeignKey(ec => ec.OwnerUserId);

            builder.HasOne<Post>(ec => ec.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(ec => ec.PostId)
                .IsRequired();
        }
    }
}
