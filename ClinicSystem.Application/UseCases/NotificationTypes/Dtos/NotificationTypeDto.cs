namespace ClinicSystem.Application.UseCases.NotificationTypes.Dtos;

public class NotificationTypeDto
{
    public Guid TypeId { get; set; } = Guid.Empty;

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? TemplateSubject { get; set; } = null;

    public string? TemplateBody { get; set; } = null;

    public bool? IsActive { get; set; } = null;
}

