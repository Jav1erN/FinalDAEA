using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Appointments.Dtos;

public static class AppointmentMappings
{
    public static AppointmentDto ToDto(this Appointment entity)
    {
        return new AppointmentDto
        {
            AppointmentId = entity.AppointmentId,
            PatientId = entity.PatientId,
            DoctorId = entity.DoctorId,
            StatusId = entity.StatusId,
            AppointmentDate = entity.AppointmentDate,
            DurationMinutes = entity.DurationMinutes,
            Reason = entity.Reason,
            Notes = entity.Notes,
            CancellationReason = entity.CancellationReason,
            RescheduledFrom = entity.RescheduledFrom,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}
