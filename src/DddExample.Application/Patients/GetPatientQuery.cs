using System;
using DddExample.Common.Cqrs;

namespace DddExample.Application.Patients
{
    public class GetPatientQuery : IQuery
    {
        public GetPatientQuery(Guid patientId)
        {
            PatientId = patientId;
        }

        public Guid PatientId { get; }
    }
}