using DddExample.Data.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddExample.Data.Ef.EntityTypeConfigurations
{
    internal class PatientTableEntityTypeConfiguration : IEntityTypeConfiguration<PatientTable>
    {
        public void Configure(EntityTypeBuilder<PatientTable> builder)
        {
            builder.ToTable("Patient");
            builder.HasKey(x => x.PatientId);
            builder.Property(x => x.Name);
        }
    }
}