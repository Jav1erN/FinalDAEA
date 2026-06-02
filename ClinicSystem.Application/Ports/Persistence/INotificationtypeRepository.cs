using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Ports.Persistence;

public interface INotificationTypeRepository : IRepository<NotificationType>
{
    Task<NotificationType?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);
}