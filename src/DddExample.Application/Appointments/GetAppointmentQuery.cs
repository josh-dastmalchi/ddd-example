using System;
using DddExample.Common.Cqrs;

namespace DddExample.Application.Appointments
{
    public class GetAppointmentQuery : IQuery
    {
        public GetAppointmentQuery(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }

        public Guid AppointmentId { get; }
    }
}