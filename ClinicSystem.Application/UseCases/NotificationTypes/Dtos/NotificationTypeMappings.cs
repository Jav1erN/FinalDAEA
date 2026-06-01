using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Dtos;

public static class NotificationTypeMappings
{
    public static NotificationTypeDto ToDto(this NotificationType entity)
    {
        return new NotificationTypeDto
        {
            TypeId = entity.TypeId,
            Code = entity.Code,
            Name = entity.Name,
            TemplateSubject = entity.TemplateSubject,
            TemplateBody = entity.TemplateBody,
            IsActive = entity.IsActive
        };
    }
}
