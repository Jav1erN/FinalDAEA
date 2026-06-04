using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Domain.Ports.Repositories;

public interface INotificationTypeRepository : IRepository<NotificationType>
{
    Task<NotificationType?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);
}