using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class AppointmentStatusMappings
{
    public static AppointmentStatusDto ToDto(this AppointmentStatus entity)
    {
        return new AppointmentStatusDto
        {
            StatusId = entity.StatusId,
            Name = entity.Name,
            Description = entity.Description
        };
    }
}

