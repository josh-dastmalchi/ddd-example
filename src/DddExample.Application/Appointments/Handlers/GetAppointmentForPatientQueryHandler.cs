using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Application.Appointments.ReadModels;
using DddExample.Common;
using DddExample.Common.Cqrs;
using DddExample.Data.ReadModels;

namespace DddExample.Application.Appointments.Handlers
{
    public class GetAppointmentForPatientQueryHandler : IQueryHandlerAsync<GetAppointmentsForPatientQuery, IReadOnlyList<AppointmentReadModel>>
    {
        private readonly IReadModelData _readModelData;

        public GetAppointmentForPatientQueryHandler(IReadModelData readModelData)
        {
            _readModelData = readModelData;
        }
        public async Task<IReadOnlyList<AppointmentReadModel>> HandleAsync(GetAppointmentsForPatientQuery query, CancellationToken cancellationToken = default)
        {
            var patientTables = await _readModelData.GetPatientTablesAsync(cancellationToken);
            var patientTable = patientTables.SingleOrDefault(x => x.PatientId == query.PatientId);
            if (patientTable == null)
            {
                throw new EntityNotFoundException($"The requested patient ({query.PatientId}) was not found.");
            }
            var appointmentTables = await _readModelData.GetAppointmentTablesAsync(cancellationToken);

            return appointmentTables.Where(x => x.PatientId == query.PatientId).Select(x => new AppointmentReadModel
            {
                AppointmentAt = x.AppointmentAt,
                DoctorId = x.DoctorId,
                AppointmentId = x.AppointmentId,
                AppointmentDurationInMinutes = x.Duration,
                PatientId = x.PatientId
            }).ToList();
        }
    }
}