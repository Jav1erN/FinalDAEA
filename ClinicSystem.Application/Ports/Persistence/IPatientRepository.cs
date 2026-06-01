using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Ports.Persistence;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByDocumentNumberAsync(
        string documentNumber,
        CancellationToken cancellationToken = default);
}
