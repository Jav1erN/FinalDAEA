using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class BillingRepository : Repository<Billing>, IBillingRepository
{
    public BillingRepository(ClinicDbContext context) : base(context)
    {
    }
}
