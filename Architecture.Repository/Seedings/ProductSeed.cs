using Architecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Repository.Seedings
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                Name = "Buz dolabı",
                CategoryId = 1,
                Price = 12475,
                Stock = 180,
                CreatedDate = DateTime.Now
            }, new Product
            {
                Id = 2,
                Name = "lenovo i5 8gb ram",
                CategoryId = 2,
                Price = 17500,
                Stock = 1400,
                CreatedDate = DateTime.Now
            }, new Product
            {
                Id = 3,
                Name = "xioami not 9s",
                CategoryId = 3,
                Price = 9999,
                Stock = 475,
                CreatedDate = DateTime.Now
            }, new Product
            {
                Id = 4,
                Name = "ana kart nvidia G2040",
                CategoryId = 4,
                Price = 4899,
                Stock = 78,
                CreatedDate = DateTime.Now
            });
        }
    }
}
