using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class AuditLogMappings
{
    public static AuditLogDto ToDto(this AuditLog entity)
    {
        return new AuditLogDto
        {
            AuditLogId = entity.AuditLogId,
            UserId = entity.UserId,
            Action = entity.Action,
            EntityName = entity.EntityName,
            EntityId = entity.EntityId,
            OldValues = entity.OldValues,
            NewValues = entity.NewValues,
            UserAgent = entity.UserAgent,
            CorrelationId = entity.CorrelationId,
            CreatedAt = entity.CreatedAt
        };
    }
}

