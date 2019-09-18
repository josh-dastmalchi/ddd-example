using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Application.Doctors.ReadModels;
using DddExample.Common;
using DddExample.Common.Cqrs;
using DddExample.Data.ReadModels;

namespace DddExample.Application.Doctors.Handlers
{
    public class GetDoctorQueryHandler : IQueryHandlerAsync<GetDoctorQuery, DoctorReadModel>
    {
        private readonly IReadModelData _readModelData;

        public GetDoctorQueryHandler(IReadModelData readModelData)
        {
            _readModelData = readModelData;
        }

        public async Task<DoctorReadModel> HandleAsync(GetDoctorQuery query, CancellationToken cancellationToken = default)
        {
            var doctors = await _readModelData.GetDoctorTablesAsync(cancellationToken);
            var doctorTable = doctors.SingleOrDefault(x => x.DoctorId == query.DoctorId);

            if (doctorTable == null)
            {
                throw new EntityNotFoundException($"The requested doctor ({query.DoctorId}) was not found.");
            }

            var doctorAvailabilities = await _readModelData.GetDoctorAvailabilityTablesAsync(cancellationToken);

            return new DoctorReadModel
            {
                DoctorId = doctorTable.DoctorId,
                Name = doctorTable.Name,
                Availabilities = doctorAvailabilities.Where(y => y.DoctorId == query.DoctorId).Select(y => new DoctorAvailabilityReadModel
                {
                    DayOfWeek = y.DayOfWeek.ToString("G"),
                    EndsAt = y.EndsAt,
                    DoctorAvailabilityId = y.DoctorAvailabilityId,
                    StartsAt = y.StartsAt
                }).ToList()
            };
        }
    }
}
