using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class InsurancePolicyRepository : Repository<InsurancePolicy>, IInsurancePolicyRepository
{
    public InsurancePolicyRepository(ClinicDbContext context) : base(context)
    {
    }
}
