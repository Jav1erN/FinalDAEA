using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class SpecialtyRepository : Repository<Specialty>, ISpecialtyRepository
{
    public SpecialtyRepository(ClinicDbContext context) : base(context)
    {
    }
}
