using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class NotificationTypeRepository : Repository<NotificationType>, INotificationTypeRepository
{
    public NotificationTypeRepository(ClinicDbContext context) : base(context)
    {
    }
}
