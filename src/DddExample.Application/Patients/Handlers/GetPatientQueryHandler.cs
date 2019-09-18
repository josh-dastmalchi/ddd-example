using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Application.Patients.ReadModels;
using DddExample.Common;
using DddExample.Common.Cqrs;
using DddExample.Data.ReadModels;

namespace DddExample.Application.Patients.Handlers
{
    public class GetPatientQueryHandler : IQueryHandlerAsync<GetPatientQuery, PatientReadModel>
    {
        private readonly IReadModelData _readModelData;

        public GetPatientQueryHandler(IReadModelData readModelData)
        {
            _readModelData = readModelData;
        }

        public async Task<PatientReadModel> HandleAsync(GetPatientQuery query,
            CancellationToken cancellationToken = default)
        {
            var patientTables = await _readModelData.GetPatientTablesAsync(cancellationToken);

            var patientTable = patientTables.SingleOrDefault(x => x.PatientId == query.PatientId);
            if (patientTable == null)
            {
                throw new EntityNotFoundException($"The requested patient ({query.PatientId}) was not found.");
            }

            return new PatientReadModel
            {
                PatientId = patientTable.PatientId,
                Name = patientTable.Name
            };
        }
    }
}