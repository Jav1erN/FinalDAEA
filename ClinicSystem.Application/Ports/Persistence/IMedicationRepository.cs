using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Ports.Persistence;

public interface IMedicationRepository : IRepository<Medication>
{
    Task<IEnumerable<Medication>> GetActiveAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<Medication>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default);
}