using System;
using DddExample.Common.Cqrs;

namespace DddExample.Application.Appointments
{
    public class GetAppointmentsForPatientQuery : IQuery
    {
        public GetAppointmentsForPatientQuery(Guid patientId)
        {
            PatientId = patientId;
        }

        public Guid PatientId { get; }
    }
}