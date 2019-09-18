using DddExample.Common.UnitOfWork;
using DddExample.Data.Ef;
using DddExample.Data.Ef.ReadModels;
using DddExample.Data.Ef.Repositories;
using DddExample.Data.Ef.UnitOfWork;
using DddExample.Data.ReadModels;
using DddExample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DddExampleDataEfServiceCollectionExtensions
    {
        public static IServiceCollection AddExampleDataEf(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IReadModelData, EfReadModelData>();

            // We manage the dbcontext lifetime via unit of work so override the default scoped service lifetime
            services.AddDbContext<DddExampleDbContext>(dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(connectionString), ServiceLifetime.Transient); 

            // Unit of work
            services.AddScoped<EntityFrameworkUnitOfWorkFactory<DddExampleDbContext>>();
            services.AddScoped<IUnitOfWorkFactory>(serviceProvider =>
                serviceProvider.GetRequiredService<EntityFrameworkUnitOfWorkFactory<DddExampleDbContext>>());
            services.AddScoped<IDbContextProvider<DddExampleDbContext>>(serviceProvider =>
                serviceProvider.GetRequiredService<EntityFrameworkUnitOfWorkFactory<DddExampleDbContext>>());

            // Repositories
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();

            return services;
        }
    }
}