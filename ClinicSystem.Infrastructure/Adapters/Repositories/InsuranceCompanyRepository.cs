using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class InsuranceCompanyRepository : Repository<InsuranceCompany>, IInsuranceCompanyRepository
{
    public InsuranceCompanyRepository(ClinicDbContext context) : base(context)
    {
    }
}
