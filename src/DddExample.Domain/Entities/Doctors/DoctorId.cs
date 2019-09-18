using System;
using System.Collections.Generic;
using DddExample.Common.Ddd;

namespace DddExample.Domain.Entities.Doctors
{
    public class DoctorId : ValueObject<DoctorId>
    {
        public DoctorId(Guid value)
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
