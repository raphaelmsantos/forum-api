using Forum.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            builder.Property(c => c.Active).HasColumnName("Active").IsRequired();
            builder.Property(c => c.InsertDate).HasColumnName("InsertDate");
        }
    }
}
