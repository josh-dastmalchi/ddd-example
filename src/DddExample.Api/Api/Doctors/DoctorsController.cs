using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DddExample.Application.Doctors;
using DddExample.Application.Doctors.ReadModels;
using DddExample.Common.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace DddExample.Api.Api.Doctors
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IQueryHandlerAsync<GetDoctorQuery, DoctorReadModel> _getDoctorQueryHandler;
        private readonly IQueryHandlerAsync<GetAllDoctorsQuery, IReadOnlyList<AllDoctorsReadModel>> _getAllDoctorsQueryHandler;

        public DoctorsController(
            IQueryHandlerAsync<GetDoctorQuery, DoctorReadModel> getDoctorQueryHandler,
            IQueryHandlerAsync<GetAllDoctorsQuery, IReadOnlyList<AllDoctorsReadModel>> getAllDoctorsQueryHandler
            )
        {
            _getDoctorQueryHandler = getDoctorQueryHandler;
            _getAllDoctorsQueryHandler = getAllDoctorsQueryHandler;
        }

        [HttpGet("{doctorId}")]
        public async Task<ActionResult<DoctorReadModel>> GetById(Guid doctorId)
        {
            var query = new GetDoctorQuery(doctorId);
            var doctorReadModel = await _getDoctorQueryHandler.HandleAsync(query);

            return Ok(doctorReadModel);

        }

        [HttpGet("")]
        public async Task<ActionResult<AllDoctorsReadModel>> GetAll()
        {
            var query = new GetAllDoctorsQuery();
            var allDoctorsReadModel = await _getAllDoctorsQueryHandler.HandleAsync(query);

            return Ok(allDoctorsReadModel);
        }
    }
}