using DddExample.Data.Schema;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Data.Ef
{
    public class DddExampleDbContext : DbContext
    {
        public DddExampleDbContext()
        {
        }

        public DddExampleDbContext(DbContextOptions<DddExampleDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<AppointmentTable> AppointmentTables { get; set; }
        public DbSet<DoctorAvailabilityTable> DoctorAvailabilityTables { get; set; }
        public DbSet<DoctorTable> DoctorTables { get; set; }
        public DbSet<PatientTable> PatientTables { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}