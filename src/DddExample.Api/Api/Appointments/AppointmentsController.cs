using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DddExample.Api.Api.Appointments.Requests;
using DddExample.Application.Appointments;
using DddExample.Application.Appointments.ReadModels;
using DddExample.Common.Cqrs;
using DddExample.Common.Identifiers;
using Microsoft.AspNetCore.Mvc;

namespace DddExample.Api.Api.Appointments
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IIdentifierGenerationService _identifierGenerationService;
        private readonly ICommandHandlerAsync<ScheduleAppointmentCommand> _scheduleAppointmentCommandHandler;
        private readonly IQueryHandlerAsync<GetAppointmentQuery, AppointmentReadModel> _getAppointmentQueryHandler;
        private readonly IQueryHandlerAsync<GetAppointmentsForDoctorQuery, IReadOnlyList<AppointmentReadModel>> _getAllAppointmentsForDoctorQueryHandler;
        private readonly IQueryHandlerAsync<GetAppointmentsForPatientQuery, IReadOnlyList<AppointmentReadModel>> _getAllAppointmentsForPatientQueryHandler;

        public AppointmentsController(
            IIdentifierGenerationService identifierGenerationService, 
            ICommandHandlerAsync<ScheduleAppointmentCommand> scheduleAppointmentCommandHandler,
            IQueryHandlerAsync<GetAppointmentQuery, AppointmentReadModel> getAppointmentQueryHandler,
            IQueryHandlerAsync<GetAppointmentsForDoctorQuery, IReadOnlyList<AppointmentReadModel>> getAllAppointmentsForDoctorQueryHandler,
            IQueryHandlerAsync<GetAppointmentsForPatientQuery, IReadOnlyList<AppointmentReadModel>> getAllAppointmentsForPatientQueryHandler)
        {
            _identifierGenerationService = identifierGenerationService;
            _scheduleAppointmentCommandHandler = scheduleAppointmentCommandHandler;
            _getAppointmentQueryHandler = getAppointmentQueryHandler;
            _getAllAppointmentsForDoctorQueryHandler = getAllAppointmentsForDoctorQueryHandler;
            _getAllAppointmentsForPatientQueryHandler = getAllAppointmentsForPatientQueryHandler;
        }

        [HttpGet("{appointmentId}")]
        
        public async Task<ActionResult<AppointmentReadModel>> GetById(Guid appointmentId)
        {
            var query = new GetAppointmentQuery(appointmentId);

            var appointmentReadModel = await _getAppointmentQueryHandler.HandleAsync(query);

            return Ok(appointmentReadModel);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IReadOnlyList<AppointmentReadModel>>> GetAllForDoctor(Guid doctorId)
        {
            var query = new GetAppointmentsForDoctorQuery(doctorId);
            var appointmentReadModels = await _getAllAppointmentsForDoctorQueryHandler.HandleAsync(query);

            return Ok(appointmentReadModels);

        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IReadOnlyList<AppointmentReadModel>>> GetAllForPatient(Guid patientId)
        {
            var query = new GetAppointmentsForPatientQuery(patientId);
            var appointmentReadModels = await _getAllAppointmentsForPatientQueryHandler.HandleAsync(query);

            return Ok(appointmentReadModels);
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleAppointment(ScheduleAppointmentRequest request)
        {
            var appointmentId = _identifierGenerationService.Generate();
            var command = new ScheduleAppointmentCommand(appointmentId, request.DoctorId, request.PatientId, request.AppointmentAt, request.AppointmentDurationInMinutes);
            await _scheduleAppointmentCommandHandler.HandleAsync(command);

            return CreatedAtAction(nameof(GetById), new {AppointmentId = appointmentId},null);
        }
    }
}