using System;
using DddExample.Common.Cqrs;

namespace DddExample.Application.Appointments
{
    public class GetAppointmentsForDoctorQuery : IQuery
    {
        public GetAppointmentsForDoctorQuery(Guid doctorId)
        {
            DoctorId = doctorId;
        }

        public Guid DoctorId { get; }
    }
}