using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class LaboratoryTestRepository : Repository<LaboratoryTest>, ILaboratoryTestRepository
{
    public LaboratoryTestRepository(ClinicDbContext context) : base(context)
    {
    }
}
