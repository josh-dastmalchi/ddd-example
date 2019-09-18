using System;
using DddExample.Data.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddExample.Data.Ef.EntityTypeConfigurations
{
    internal class AppointmentTableEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentTable>
    {
        public void Configure(EntityTypeBuilder<AppointmentTable> builder)
        {
            builder.ToTable("Appointment");
            builder.HasKey(x => x.AppointmentId);
            builder.HasOne<DoctorTable>().WithMany().HasForeignKey(x => x.DoctorId);
            builder.HasOne<PatientTable>().WithMany().HasForeignKey(x => x.PatientId);
            builder.Property(x => x.AppointmentAt);
            builder.Property(x => x.Duration);
        }
    }
}
