using System;

namespace DddExample.Application.Patients.ReadModels
{
    public class PatientReadModel
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; }
    }
}