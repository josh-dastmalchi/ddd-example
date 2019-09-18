using DddExample.Data.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddExample.Data.Ef.EntityTypeConfigurations
{
    public class DoctorAvailabilityTableEntityTypeConfiguration : IEntityTypeConfiguration<DoctorAvailabilityTable>
    {
        public void Configure(EntityTypeBuilder<DoctorAvailabilityTable> builder)
        {
            builder.ToTable("DoctorAvailability");
            builder.HasKey(x => x.DoctorAvailabilityId);
            builder.HasOne<DoctorTable>().WithMany().HasForeignKey(x => x.DoctorId);
            builder.Property(x => x.DayOfWeek);
            builder.Property(x => x.StartsAt);
            builder.Property(x => x.EndsAt);
        }
    }
}