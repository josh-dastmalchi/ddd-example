using System;
using DddExample.Common.Cqrs;

namespace DddExample.Application.Doctors
{
    public class GetDoctorQuery : IQuery
    {
        public GetDoctorQuery(Guid doctorId)
        {
            DoctorId = doctorId;
        }

        public Guid DoctorId { get; }
    }
}