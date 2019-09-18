using System.Threading;
using System.Threading.Tasks;
using DddExample.Common.Cqrs;
using DddExample.Common.UnitOfWork;
using DddExample.Domain.Entities.Appointments;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Entities.Patients;
using DddExample.Domain.Services;

namespace DddExample.Application.Appointments.Handlers
{
    internal class ScheduleAppointmentCommandHandler : ICommandHandlerAsync<ScheduleAppointmentCommand>
    {
        private readonly IScheduleAppointmentService _scheduleAppointmentService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ScheduleAppointmentCommandHandler(IScheduleAppointmentService scheduleAppointmentService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _scheduleAppointmentService = scheduleAppointmentService;
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task HandleAsync(ScheduleAppointmentCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var unitOfWork = await _unitOfWorkFactory.CreateAsync())
            {
                // Convert from external representations to domain representations
                var appointmentId = new AppointmentId(command.AppointmentId);
                var doctorId = new DoctorId(command.DoctorId);
                var patientId = new PatientId(command.PatientId);
                var appointmentDuration = new AppointmentDuration(command.AppointmentDurationInMinutes);

                // Invoke domain logic
                await _scheduleAppointmentService.ScheduleAsync(appointmentId, doctorId, patientId, command.AppointmentAt,
                    appointmentDuration, cancellationToken);

                await unitOfWork.CommitAsync();
            }
        }
    }
}