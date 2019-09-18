using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Application.Doctors.ReadModels;
using DddExample.Common.Cqrs;
using DddExample.Data.ReadModels;

namespace DddExample.Application.Doctors.Handlers
{
    public class GetAllDoctorsQueryHandler : IQueryHandlerAsync<GetAllDoctorsQuery, IReadOnlyList<AllDoctorsReadModel>>
    {
        private readonly IReadModelData _readModelData;

        public GetAllDoctorsQueryHandler(IReadModelData readModelData)
        {
            _readModelData = readModelData;
        }

        public async Task<IReadOnlyList<AllDoctorsReadModel>> HandleAsync(GetAllDoctorsQuery query, CancellationToken cancellationToken = default)
        {
            var doctors = await _readModelData.GetDoctorTablesAsync(cancellationToken);

            return doctors.Select(x => new AllDoctorsReadModel
            {
                DoctorId = x.DoctorId,
                Name = x.Name
            }).ToList();
        }
    }
}