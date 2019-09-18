using System;
using DddExample.Common.Identifiers;
using DddExample.Data.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Data.Ef.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", false, true)
                .Build();
            using (var serviceProvider = RegisterDependencies(configuration))
            {
                var configurationDbContext = serviceProvider.GetRequiredService<DddExampleDbContext>();
                configurationDbContext.Database.Migrate();

                var doctorId1 = GuidComb.Generate();

                configurationDbContext.DoctorTables.Add(new DoctorTable
                {
                    DoctorId = doctorId1,
                    Name = "Dr. Hard Toget"
                });

                configurationDbContext.DoctorAvailabilityTables.Add(new DoctorAvailabilityTable
                {
                    DoctorAvailabilityId = GuidComb.Generate(),
                    DoctorId = doctorId1,
                    DayOfWeek = DayOfWeek.Monday,
                    StartsAt = TimeSpan.FromHours(13),
                    EndsAt = TimeSpan.FromHours(15),
                });

                configurationDbContext.DoctorAvailabilityTables.Add(new DoctorAvailabilityTable
                {
                    DoctorAvailabilityId = GuidComb.Generate(),
                    DoctorId = doctorId1,
                    DayOfWeek = DayOfWeek.Tuesday,
                    StartsAt = TimeSpan.FromHours(13),
                    EndsAt = TimeSpan.FromHours(15),
                });

                var doctorId2 = GuidComb.Generate();
                configurationDbContext.DoctorTables.Add(new DoctorTable
                {
                    DoctorId = doctorId2,
                    Name = "Dr. Easy Tosee"
                });

                configurationDbContext.DoctorAvailabilityTables.Add(new DoctorAvailabilityTable
                {
                    DoctorAvailabilityId = GuidComb.Generate(),
                    DoctorId = doctorId2,
                    DayOfWeek = DayOfWeek.Monday,
                    StartsAt = TimeSpan.FromHours(9),
                    EndsAt = TimeSpan.FromHours(18),
                });

                configurationDbContext.DoctorAvailabilityTables.Add(new DoctorAvailabilityTable
                {
                    DoctorAvailabilityId = GuidComb.Generate(),
                    DoctorId = doctorId2,
                    DayOfWeek = DayOfWeek.Tuesday,
                    StartsAt = TimeSpan.FromHours(9),
                    EndsAt = TimeSpan.FromHours(18),
                });

                configurationDbContext.DoctorAvailabilityTables.Add(new DoctorAvailabilityTable
                {
                    DoctorAvailabilityId = GuidComb.Generate(),
                    DoctorId = doctorId2,
                    DayOfWeek = DayOfWeek.Wednesday,
                    StartsAt = TimeSpan.FromHours(9),
                    EndsAt = TimeSpan.FromHours(18),
                });

                configurationDbContext.DoctorAvailabilityTables.Add(new DoctorAvailabilityTable
                {
                    DoctorAvailabilityId = GuidComb.Generate(),
                    DoctorId = doctorId2,
                    DayOfWeek = DayOfWeek.Thursday,
                    StartsAt = TimeSpan.FromHours(9),
                    EndsAt = TimeSpan.FromHours(18),
                });

                configurationDbContext.DoctorAvailabilityTables.Add(new DoctorAvailabilityTable
                {
                    DoctorAvailabilityId = GuidComb.Generate(),
                    DoctorId = doctorId2,
                    DayOfWeek = DayOfWeek.Friday,
                    StartsAt = TimeSpan.FromHours(9),
                    EndsAt = TimeSpan.FromHours(18),
                });

                var patientId = GuidComb.Generate();

                configurationDbContext.PatientTables.Add(new PatientTable
                {
                    PatientId = patientId,
                    Name = "Emile Agin"
                });

                configurationDbContext.SaveChanges();
            }
        }

        private static ServiceProvider RegisterDependencies(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            var connectionString = configuration.GetConnectionString("DddExampleConnectionString");

            services.AddDbContext<DddExampleDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            return services.BuildServiceProvider();
        }
    }
}