using System;
using System.Collections.Generic;
using DddExample.Common.Ddd;

namespace DddExample.Domain.Entities.Patients
{
    public class PatientId : ValueObject<PatientId>
    {
        public PatientId(Guid value)
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
