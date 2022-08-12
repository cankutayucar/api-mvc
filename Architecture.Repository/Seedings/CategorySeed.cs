using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Repository.Seedings
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                Id = 1,
                Name = "Elektronik"
            }, new Category
            {
                Id = 2,
                Name = "Bilgisayar"
            }, new Category
            {
                Id = 3,
                Name = "Telefon"
            }, new Category
            {
                Id = 4,
                Name = "Bilgisayar Parçaları"
            });
        }
    }
}
