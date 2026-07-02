using ClinicSystem.Domain.Common;

namespace ClinicSystem.Domain.Ports.Services;

public interface IAppointmentSchedulingService
{
    Task<BusinessRuleResult> EnsureCanScheduleAsync(
        Guid doctorId,
        DateTime appointmentDate,
        int? durationMinutes,
        Guid? appointmentId = null,
        CancellationToken cancellationToken = default);
}
