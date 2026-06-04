using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Adapters.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(ClinicDbContext context) : base(context)
    {
    }

    public async Task<Patient?> GetByDocumentNumberAsync(
        string documentNumber,
        CancellationToken cancellationToken = default)
    {
        return await Context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DocumentNumber == documentNumber, cancellationToken);
    }
}
