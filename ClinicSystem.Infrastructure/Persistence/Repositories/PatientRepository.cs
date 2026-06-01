using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

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
