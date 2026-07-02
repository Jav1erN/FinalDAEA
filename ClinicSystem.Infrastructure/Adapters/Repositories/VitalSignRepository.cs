using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class VitalSignRepository : Repository<VitalSign>, IVitalSignRepository
{
    public VitalSignRepository(ClinicDbContext context) : base(context)
    {
    }
}
