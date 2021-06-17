namespace BlazorShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static ModelConstants.Common;
    using static ModelConstants.category;
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category
                .Property(c => c.Name)
                .HasMaxLength(MaxNameLength)
                .IsRequired();
            category
               .Property(p => p.ImageSource)
               .HasMaxLength(MaxUrlLength)
               .IsRequired();
            category
                .HasIndex(c => c.IsDeleted);

            category
                .HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
