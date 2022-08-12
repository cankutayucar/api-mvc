using Architecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Repository.Mappings
{
    public class ProductFeatureMap : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(pf => pf.Id);
            builder.Property(pf => pf.Id).UseIdentityColumn(1, 1);
        }
    }
}
