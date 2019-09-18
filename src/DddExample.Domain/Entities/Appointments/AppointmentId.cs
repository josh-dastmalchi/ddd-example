using System;
using System.Collections.Generic;
using System.Text;
using DddExample.Common.Ddd;

namespace DddExample.Domain.Entities.Appointments
{
    public class AppointmentId : ValueObject<AppointmentId>
    {
        public AppointmentId(Guid value)
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
