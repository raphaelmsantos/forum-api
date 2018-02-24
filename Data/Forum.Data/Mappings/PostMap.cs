using Forum.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title).HasColumnName("Title").IsRequired().HasMaxLength(100);
            builder.Property(c => c.Content).HasColumnName("Content").IsRequired().HasMaxLength(8000);
            builder.Property(c => c.Active).HasColumnName("Active").IsRequired();
            builder.Property(c => c.InsertDate).HasColumnName("InsertDate");
            builder.Property(c => c.OwnerUserId).HasColumnName("OwnerUserId").IsRequired();
            builder.Property(c => c.CategoryId).HasColumnName("CategoryId").IsRequired();

            builder.HasOne<User>(ec => ec.OwnerUser)
                .WithMany(c => c.Posts)
                .HasForeignKey(ec => ec.OwnerUserId);

            builder.HasOne<Category>(ec => ec.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(ec => ec.CategoryId);
        }
    }
}
