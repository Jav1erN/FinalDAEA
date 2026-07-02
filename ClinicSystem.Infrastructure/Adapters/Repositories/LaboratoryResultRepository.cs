using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class LaboratoryResultRepository : Repository<LaboratoryResult>, ILaboratoryResultRepository
{
    public LaboratoryResultRepository(ClinicDbContext context) : base(context)
    {
    }
}
