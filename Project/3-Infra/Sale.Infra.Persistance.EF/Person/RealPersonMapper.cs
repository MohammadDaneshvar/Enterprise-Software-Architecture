using Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sale.Infra.Persistance.Nh.Order
{

    public class RealPersonConfiguration : IEntityTypeConfiguration<RealPerson>
    {
        public void Configure(EntityTypeBuilder<RealPerson> builder)
        {
            builder.HasKey(x=>x.RealPersonID);
        }
    }
}