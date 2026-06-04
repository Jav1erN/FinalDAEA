using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;

namespace ClinicSystem.Domain.Ports.Repositories;

public interface IMedicationRepository : IRepository<Medication>
{
    Task<IEnumerable<Medication>> GetActiveAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<Medication>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default);
}