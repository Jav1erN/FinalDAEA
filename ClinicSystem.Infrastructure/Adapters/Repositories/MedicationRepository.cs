using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class MedicationRepository : Repository<Medication>, IMedicationRepository
{
    public MedicationRepository(ClinicDbContext context) : base(context)
    {
    }
}
