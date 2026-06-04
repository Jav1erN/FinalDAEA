using ClinicSystem.Domain.Ports.Repositories;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class MedicationRepository : Repository<Medication>, IMedicationRepository
{
    public MedicationRepository(ClinicDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Medication>> GetActiveAsync(
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(m => m.IsActive == true && m.DeletedAt == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Medication>> GetLowStockAsync(
        int threshold,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(m => m.Stock.HasValue
                        && m.Stock.Value <= threshold
                        && m.DeletedAt == null)
            .ToListAsync(cancellationToken);
    }
}