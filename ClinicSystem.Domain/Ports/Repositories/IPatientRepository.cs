using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Domain.Ports.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByDocumentNumberAsync(
        string documentNumber,
        CancellationToken cancellationToken = default);
}
