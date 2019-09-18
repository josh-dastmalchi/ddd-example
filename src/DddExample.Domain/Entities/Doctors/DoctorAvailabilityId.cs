using System;
using System.Collections.Generic;
using DddExample.Common.Ddd;

namespace DddExample.Domain.Entities.Doctors
{
    public class DoctorAvailabilityId : ValueObject<DoctorAvailabilityId>
    {
        public DoctorAvailabilityId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new object[] {Value};
        }
    }
}
