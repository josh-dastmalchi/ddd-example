using DddExample.Common.Identifiers;
using DddExample.Common.Times;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class CommonServiceCollectionExtensions
    {
        public static IServiceCollection AddExampleCommon(this IServiceCollection services)
        {
            services.AddTransient<IIdentifierGenerationService, GuidCombIdentifierGenerationService>();
            services.AddTransient<ISystemClockService, StandardSystemClockService>();

            return services;
        }

    }
}