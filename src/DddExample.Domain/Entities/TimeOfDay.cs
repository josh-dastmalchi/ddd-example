using System;
using System.Collections.Generic;
using DddExample.Common.Ddd;

namespace DddExample.Domain.Entities
{
    public class TimeOfDay : ValueObject<TimeOfDay>
    {
        private static readonly TimeSpan InclusiveMinimum;
        private static readonly TimeSpan ExclusiveMaximum;
        
        static TimeOfDay()
        {
            InclusiveMinimum = TimeSpan.Zero;
            ExclusiveMaximum = new TimeSpan(24, 0, 0);
        }

        public TimeOfDay(TimeSpan value)
        {
            if (value != null && value >= ExclusiveMaximum)
            {
                throw new DomainValidationException($"The maximum allowed time must be less than {ExclusiveMaximum}.");
            }

            if (value != null && value < InclusiveMinimum)
            {
                throw new DomainValidationException($"The minimum time is {InclusiveMinimum}");
            }

            Value = value;
        }
        public TimeSpan Value { get; }
        
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] {Value};
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool operator >(TimeOfDay a, TimeOfDay b)
        {
            return a?.Value > b?.Value;
        }

        public static bool operator <(TimeOfDay a, TimeOfDay b)
        {
            return a?.Value < b?.Value;
        }

        public static bool operator >=(TimeOfDay a, TimeOfDay b)
        {
            return a?.Value >= b?.Value;
        }

        public static bool operator <=(TimeOfDay a, TimeOfDay b)
        {
            return a?.Value <= b?.Value;
        }

        public static implicit operator TimeSpan(TimeOfDay timeOfDay)
        {
            return timeOfDay.Value;
        }

    }
}
