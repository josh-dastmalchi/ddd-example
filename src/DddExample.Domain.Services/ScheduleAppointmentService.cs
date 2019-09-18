using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Common;
using DddExample.Common.Times;
using DddExample.Domain.Entities.Appointments;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Entities.Patients;
using DddExample.Domain.Repositories;

namespace DddExample.Domain.Services
{
    public class ScheduleAppointmentService : IScheduleAppointmentService
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ISystemClockService _systemClockService;

        public ScheduleAppointmentService(
            IDoctorAvailabilityRepository doctorAvailabilityRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IAppointmentRepository appointmentRepository,
            ISystemClockService systemClockService)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;
            _systemClockService = systemClockService;
        }

        public async Task ScheduleAsync(AppointmentId appointmentId, DoctorId doctorId, PatientId patientId, DateTime requestedAppointmentAt, AppointmentDuration requestedAppointmentDuration,
            CancellationToken cancellationToken = default)
        {
            if (requestedAppointmentAt < _systemClockService.UtcNow.DateTime)
            {
                throw new DomainValidationException("The appointment could not be scheduled because it occurs in the past.");
            }

            var doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);

            if (doctor == null)
            {
                throw new EntityNotFoundException("The requested doctor was not found.");
            }

            var patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);

            if (patient == null)
            {
                throw new EntityNotFoundException("The requested patient was not found.");
            }

            // Does the requested appointment occur during the doctor's availability?

            var doctorAvailabilities = await _doctorAvailabilityRepository.GetAllForDoctorAsync(doctorId, cancellationToken);

            var doctorIsAvailable = doctorAvailabilities.Any(a =>
                a.DayOfWeek == requestedAppointmentAt.DayOfWeek && // Occurs on an available day of the week
                a.StartsAt <= requestedAppointmentAt.TimeOfDay // After that day of the week's start time
                && a.EndsAt >= requestedAppointmentAt.TimeOfDay + requestedAppointmentDuration); // And ends before that day of the week's end time

            if (!doctorIsAvailable)
            {
                throw new DomainValidationException("The appointment could not be scheduled because it is outside of the doctor's availability.");
            }

            // Determine if there are any scheduling conflicts.
            // Algorithm description: Two time periods overlap if each one starts before the other one ends.

            var doctorAppointments = await _appointmentRepository.GetAllUpcomingForDoctorAsync(doctorId, cancellationToken);
            
            var doctorHasConflictingAppointment = doctorAppointments.Any(existingAppointment =>
                requestedAppointmentAt < existingAppointment.AppointmentAt.Add(existingAppointment.AppointmentDuration) &&
                existingAppointment.AppointmentAt < requestedAppointmentAt.Add(requestedAppointmentDuration));

            if (doctorHasConflictingAppointment)
            {
                throw new DomainValidationException("The appointment could not be scheduled because the doctor has a conflicting appointment.");
            }

            var patientAppointments = await _appointmentRepository.GetAllUpcomingForPatientAsync(patientId, cancellationToken);

            var patientHasConflictingAppointment = patientAppointments.Any(existingAppointment =>
                requestedAppointmentAt < existingAppointment.AppointmentAt.Add(existingAppointment.AppointmentDuration) &&
                existingAppointment.AppointmentAt < requestedAppointmentAt.Add(requestedAppointmentDuration));

            if (patientHasConflictingAppointment)
            {
                throw new DomainValidationException("The appointment could not be scheduled because the patient has a conflicting appointment.");
            }

            var appointment = new Appointment(appointmentId, doctorId, patientId, requestedAppointmentAt, requestedAppointmentDuration);

            await _appointmentRepository.CreateAsync(appointment, cancellationToken);
        }
    }
}