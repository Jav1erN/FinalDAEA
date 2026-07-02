using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class PrescriptionDetailRepository : Repository<PrescriptionDetail>, IPrescriptionDetailRepository
{
    public PrescriptionDetailRepository(ClinicDbContext context) : base(context)
    {
    }
}
