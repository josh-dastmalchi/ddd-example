namespace DddExample.Domain.Entities.Doctors
{
    public class Doctor : AggregateBase
    {
        public Doctor(DoctorId doctorId, string name)
        {
            DoctorId = doctorId;
            Name = name;
        }

        public DoctorId DoctorId { get; }
        public string Name { get; }
    }
}