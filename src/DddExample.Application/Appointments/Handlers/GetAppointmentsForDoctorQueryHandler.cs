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
    public class GetAppointmentsForDoctorQueryHandler : IQueryHandlerAsync<GetAppointmentsForDoctorQuery, IReadOnlyList<AppointmentReadModel>>
    {
        private readonly IReadModelData _readModelData;

        public GetAppointmentsForDoctorQueryHandler(IReadModelData readModelData)
        {
            _readModelData = readModelData;
        }
        public async Task<IReadOnlyList<AppointmentReadModel>> HandleAsync(GetAppointmentsForDoctorQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var doctorTables = await _readModelData.GetDoctorTablesAsync(cancellationToken);
            var doctorTable = doctorTables.SingleOrDefault(x => x.DoctorId == query.DoctorId);
            if (doctorTable == null)
            {
                throw new EntityNotFoundException($"The requested doctor ({query.DoctorId}) was not found.");
            }
            var appointmentTables = await _readModelData.GetAppointmentTablesAsync(cancellationToken);

            return appointmentTables.Where(x => x.DoctorId == query.DoctorId).Select(x => new AppointmentReadModel
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