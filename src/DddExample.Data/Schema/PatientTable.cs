using System;

namespace DddExample.Data.Schema
{
    public class PatientTable
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; }
    }
}