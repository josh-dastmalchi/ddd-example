using System;
using System.Collections.Generic;

namespace DddExample.Application.Doctors.ReadModels
{
    public class DoctorReadModel
    {
        public DoctorReadModel()
        {
            Availabilities = new List<DoctorAvailabilityReadModel>();
        }
        public Guid DoctorId { get; set; }
        public string Name { get; set; }
        public IReadOnlyList<DoctorAvailabilityReadModel> Availabilities { get; set; }
    }
}