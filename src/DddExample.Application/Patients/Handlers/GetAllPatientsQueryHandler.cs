using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Application.Patients.ReadModels;
using DddExample.Common.Cqrs;
using DddExample.Data.ReadModels;

namespace DddExample.Application.Patients.Handlers
{
    public class GetAllPatientsQueryHandler : IQueryHandlerAsync<GetAllPatientsQuery, IReadOnlyList<AllPatientsReadModel>>
    {
        private readonly IReadModelData _readModelData;

        public GetAllPatientsQueryHandler(IReadModelData readModelData)
        {
            _readModelData = readModelData;
        }

        public async Task<IReadOnlyList<AllPatientsReadModel>> HandleAsync(GetAllPatientsQuery query,
            CancellationToken cancellationToken = default)
        {
            var patients = await _readModelData.GetPatientTablesAsync(cancellationToken);

            return patients.Select(x => new AllPatientsReadModel
            {
                PatientId = x.PatientId,
                Name = x.Name
            }).ToList();
        }
    }
}