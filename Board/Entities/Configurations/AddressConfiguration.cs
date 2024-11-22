using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Entities.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.OwnsOne(x => x.Coordinate, cmb =>
            {
                cmb.Property(c => c.Latitude).HasPrecision(18, 7);
                cmb.Property(c => c.Longitude).HasPrecision(18, 7);
            });
        }
    }
}
