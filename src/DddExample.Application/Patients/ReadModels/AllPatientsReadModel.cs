using System;

namespace DddExample.Application.Patients.ReadModels
{
    public class AllPatientsReadModel
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; }
    }
}
