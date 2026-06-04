using ClinicSystem.Domain.Ports.Repositories;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Adapters.Repositories;
public class NotificationTypeRepository : Repository<NotificationType>, INotificationTypeRepository
{
    public NotificationTypeRepository(ClinicDbContext context)
        : base(context)
    {
    }

    public async Task<NotificationType?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<NotificationType>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
    }
}