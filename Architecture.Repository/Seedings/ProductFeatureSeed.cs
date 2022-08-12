using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Repository.Seedings
{
    public class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(new ProductFeature
            {
                Id = 1,
                ProductId = 1,
                Height = 200,
                Width = 100,
                Color = "beyaz"
            }, new ProductFeature
            {
                Id = 2,
                ProductId = 2,
                Height = 70,
                Width = 90,
                Color = "siyah"
            }, new ProductFeature
            {
                Id = 3,
                ProductId = 3,
                Height = 22,
                Width = 15,
                Color = "mavi"
            }, new ProductFeature
            {
                Id = 4,
                ProductId = 4,
                Height = 10,
                Width = 25,
                Color = "beyaz"
            });
        }
    }
}
