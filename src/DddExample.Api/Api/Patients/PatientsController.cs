using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DddExample.Application.Patients;
using DddExample.Application.Patients.ReadModels;
using DddExample.Common.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace DddExample.Api.Api.Patients
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IQueryHandlerAsync<GetPatientQuery, PatientReadModel> _getPatientQueryHandler;
        private readonly IQueryHandlerAsync<GetAllPatientsQuery, IReadOnlyList<AllPatientsReadModel>> _getAllPatientsQueryHandler;

        public PatientsController(
            IQueryHandlerAsync<GetPatientQuery, PatientReadModel> getPatientQueryHandler,
            IQueryHandlerAsync<GetAllPatientsQuery, IReadOnlyList<AllPatientsReadModel>> getAllPatientsQueryHandler
        )
        {
            _getPatientQueryHandler = getPatientQueryHandler;
            _getAllPatientsQueryHandler = getAllPatientsQueryHandler;
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<PatientReadModel>> GetById(Guid patientId)
        {
            var query = new GetPatientQuery(patientId);
            var doctorReadModel = await _getPatientQueryHandler.HandleAsync(query);

            return Ok(doctorReadModel);

        }

        [HttpGet("")]
        public async Task<ActionResult<AllPatientsReadModel>> GetAll()
        {
            var query = new GetAllPatientsQuery();
            var allDoctorsReadModel = await _getAllPatientsQueryHandler.HandleAsync(query);

            return Ok(allDoctorsReadModel);
        }
    }
}
