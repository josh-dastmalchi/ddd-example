using DddExample.Data.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddExample.Data.Ef.EntityTypeConfigurations
{
    internal class DoctorTableEntityTypeConfiguration : IEntityTypeConfiguration<DoctorTable>
    {
        public void Configure(EntityTypeBuilder<DoctorTable> builder)
        {
            builder.ToTable("Doctor");
            builder.HasKey(x => x.DoctorId);
            builder.Property(x => x.Name);
        }
    }
}