using System;

namespace DddExample.Data.Schema
{
    public class DoctorAvailabilityTable
    {
        public Guid DoctorAvailabilityId { get; set; }
        public Guid DoctorId { get; set;}
        public DayOfWeek DayOfWeek { get;set; }
        public TimeSpan StartsAt { get; set;}
        public TimeSpan EndsAt { get; set;}
    }
}