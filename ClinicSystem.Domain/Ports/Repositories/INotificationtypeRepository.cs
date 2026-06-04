using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;

namespace ClinicSystem.Domain.Ports.Repositories;

public interface INotificationTypeRepository : IRepository<NotificationType>
{
    Task<NotificationType?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);
}