using System.Collections.Generic;
using DddExample.Application.Appointments;
using DddExample.Application.Appointments.Handlers;
using DddExample.Application.Appointments.ReadModels;
using DddExample.Application.Doctors;
using DddExample.Application.Doctors.Handlers;
using DddExample.Application.Doctors.ReadModels;
using DddExample.Application.Patients;
using DddExample.Application.Patients.Handlers;
using DddExample.Application.Patients.ReadModels;
using DddExample.Common.Cqrs;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddExampleApplication(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandlerAsync<ScheduleAppointmentCommand>, ScheduleAppointmentCommandHandler>();
            services.AddTransient<IQueryHandlerAsync<GetAppointmentQuery, AppointmentReadModel>, GetAppointmentQueryHandler>();
            services.AddTransient<IQueryHandlerAsync<GetAppointmentsForDoctorQuery, IReadOnlyList<AppointmentReadModel>>, GetAppointmentsForDoctorQueryHandler>();
            services.AddTransient<IQueryHandlerAsync<GetAppointmentsForPatientQuery, IReadOnlyList<AppointmentReadModel>>, GetAppointmentForPatientQueryHandler>();
            // Doctors
            services.AddTransient<IQueryHandlerAsync<GetDoctorQuery, DoctorReadModel>, GetDoctorQueryHandler>();
            services.AddTransient<IQueryHandlerAsync<GetAllDoctorsQuery, IReadOnlyList<AllDoctorsReadModel>>, GetAllDoctorsQueryHandler>();
            
            // Patients
            services.AddTransient<IQueryHandlerAsync<GetPatientQuery, PatientReadModel>, GetPatientQueryHandler>();
            services.AddTransient<IQueryHandlerAsync<GetAllPatientsQuery, IReadOnlyList<AllPatientsReadModel>>, GetAllPatientsQueryHandler>();
            
            return services;
        }

    }
}