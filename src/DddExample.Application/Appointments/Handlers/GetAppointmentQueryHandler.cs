using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Application.Appointments.ReadModels;
using DddExample.Common;
using DddExample.Common.Cqrs;
using DddExample.Data.ReadModels;

namespace DddExample.Application.Appointments.Handlers
{
    public class GetAppointmentQueryHandler : IQueryHandlerAsync<GetAppointmentQuery, AppointmentReadModel>
    {
        private readonly IReadModelData _readModelData;

        public GetAppointmentQueryHandler(IReadModelData readModelData)
        {
            _readModelData = readModelData;
        }


        public async Task<AppointmentReadModel> HandleAsync(GetAppointmentQuery query,
            CancellationToken cancellationToken = default)
        {
            var appointmentTables = await _readModelData.GetAppointmentTablesAsync(cancellationToken);

            var appointmentTable = appointmentTables.SingleOrDefault(x => x.AppointmentId == query.AppointmentId);

            if (appointmentTable == null)
            {
                throw new EntityNotFoundException($"The requested appointment ({query.AppointmentId}) was not found.");
            }

            return new AppointmentReadModel
            {
                AppointmentAt = appointmentTable.AppointmentAt,
                DoctorId = appointmentTable.DoctorId,
                AppointmentId = appointmentTable.AppointmentId,
                AppointmentDurationInMinutes = appointmentTable.Duration,
                PatientId = appointmentTable.PatientId
            };
        }
    }
}