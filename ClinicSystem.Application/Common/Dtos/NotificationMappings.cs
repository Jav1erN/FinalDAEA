using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class NotificationMappings
{
    public static NotificationDto ToDto(this Notification entity)
    {
        return new NotificationDto
        {
            NotificationId = entity.NotificationId,
            UserId = entity.UserId,
            TypeId = entity.TypeId,
            Channel = entity.Channel,
            Status = entity.Status,
            EntityType = entity.EntityType,
            EntityId = entity.EntityId,
            Subject = entity.Subject,
            Body = entity.Body,
            ScheduledAt = entity.ScheduledAt,
            SentAt = entity.SentAt,
            ReadAt = entity.ReadAt,
            CreatedAt = entity.CreatedAt
        };
    }
}

