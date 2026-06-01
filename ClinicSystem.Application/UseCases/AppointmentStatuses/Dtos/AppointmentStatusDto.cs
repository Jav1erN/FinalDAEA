namespace ClinicSystem.Application.UseCases.AppointmentStatuses.Dtos;

public class AppointmentStatusDto
{
    public Guid StatusId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = null;
}
