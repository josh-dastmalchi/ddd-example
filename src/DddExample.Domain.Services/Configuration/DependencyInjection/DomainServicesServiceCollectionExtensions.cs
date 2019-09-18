using DddExample.Domain.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DomainServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddExampleDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IScheduleAppointmentService, ScheduleAppointmentService>();
            return services;
        }

    }
}