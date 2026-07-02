using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Domain.Ports.Persistence;

public interface IAuditLogRepository : IRepository<AuditLog>
{
}
