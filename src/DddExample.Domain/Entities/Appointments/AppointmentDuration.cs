using System;
using System.Collections.Generic;
using DddExample.Common.Ddd;

namespace DddExample.Domain.Entities.Appointments
{
    public class AppointmentDuration : ValueObject<AppointmentDuration>
    {
        private static readonly int InclusiveMinimum;
        private static readonly int InclusiveMaximum;

        static AppointmentDuration()
        {
            InclusiveMinimum = 15;
            InclusiveMaximum = 120;
        }

        public AppointmentDuration(int numberOfMinutes)
        {
            if (numberOfMinutes > InclusiveMaximum)
            {
                throw new DomainValidationException(
                    $"The maximum allowed appointment duration is {InclusiveMaximum} minutes.");
            }

            if (numberOfMinutes < InclusiveMinimum)
            {
                throw new DomainValidationException(
                    $"The minimum allowed appointment duration is {InclusiveMinimum} minutes.");
            }

            if (numberOfMinutes % 15 != 0)
            {
                throw new DomainValidationException("Appointments may only be scheduled in increments of 15 minutes.");
            }

            DurationInMinutes = numberOfMinutes;
        }

        public TimeSpan Duration => TimeSpan.FromMinutes(DurationInMinutes);
        public int DurationInMinutes { get; }

        public static implicit operator TimeSpan(AppointmentDuration appointmentDuration)
        {
            return appointmentDuration.Duration;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] {DurationInMinutes};
        }
    }
}