using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        // Configuration of the Entity... for things like validation rules
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // This is a better approach than accepting the default
            // EF code generated in the StoreContextModelSnapshot, especially when you want to be clear about what FKs to specify, validation rules, etc... 
            // The other way of course is to annotate the properties in the class, but this keeps things together.
            // THis kind of code needs to be done before you add a migration
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)"); // Careful when using strings here. This actually won't work while we develop on sqlite, but it won't break anything
            builder.Property(p => p.PictureUrl).IsRequired();

            // EF would take care of the FK anyway, but here it is spelled out anyway...
            builder.HasOne(b => b.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(b => b.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
        }
    }
}