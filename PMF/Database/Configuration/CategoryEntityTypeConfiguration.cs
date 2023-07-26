using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMF.Database.Entities;

namespace PMF.Database.Configuration
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public CategoryEntityTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            //builder.ToTable("categories");
            // primary key
            builder.HasKey(x => x.Code);
             //definition of columns
            //builder.Property(x => x.Code).IsRequired().HasMaxLength(64);
           // builder.Property(x => x.ParentCode).HasMaxLength(64);
          // builder.Property(x => x.Name).HasMaxLength(64); 
        }

    }
}
