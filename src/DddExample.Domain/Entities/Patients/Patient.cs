using System;

namespace DddExample.Domain.Entities.Patients
{
    public class Patient : AggregateBase
    {
        public Patient(PatientId patientId, string name)
        {
            PatientId = patientId;
            Name = name;
        }

        public PatientId PatientId { get; }
        public string Name { get; }
    }
}