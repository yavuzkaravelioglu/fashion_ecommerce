using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property( p => p.Id).IsRequired();
            builder.Property( p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property( p => p.PictureUrl).IsRequired();
            builder.Property( p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property( p => p.PictureUrl1).IsRequired();
            builder.Property( p => p.PictureUrl2).IsRequired();
            builder.Property( p => p.PictureUrl3).IsRequired();
            builder.Property( p => p.PictureUrl4).IsRequired();
            builder.Property( p => p.PictureUrl5).IsRequired();
            builder.Property( p => p.PictureUrl6).IsRequired();

            //we can also arrange relationships 
            builder.HasOne( b => b.ProductBrand).WithMany() // each product has single brand, each brand can be associated with many product
                .HasForeignKey( p => p.ProductBrandId); // we also specify its foreign key

            builder.HasOne( t => t.ProductType).WithMany() // each product has single brand, each brand can be associated with many product
                .HasForeignKey( p => p.ProductTypeId); // we also specify its foreign key

        }
    }
}
