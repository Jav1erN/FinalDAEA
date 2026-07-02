using ClinicSystem.Domain.Common;
using ClinicSystem.Domain.Ports.Services;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Adapters.Services;

public class AppointmentSchedulingService : IAppointmentSchedulingService
{
    private readonly ClinicDbContext _context;

    public AppointmentSchedulingService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessRuleResult> EnsureCanScheduleAsync(
        Guid doctorId,
        DateTime appointmentDate,
        int? durationMinutes,
        Guid? appointmentId = null,
        CancellationToken cancellationToken = default)
    {
        var duration = durationMinutes ?? 30;

        if (duration <= 0)
            return BusinessRuleResult.Failure("Appointment duration must be greater than zero");

        var requestedStart = appointmentDate;
        var requestedEnd = appointmentDate.AddMinutes(duration);

        var overlaps = await _context.Appointments
            .AsNoTracking()
            .Where(appointment =>
                appointment.DoctorId == doctorId &&
                appointment.DeletedAt == null &&
                (!appointmentId.HasValue || appointment.AppointmentId != appointmentId.Value))
            .AnyAsync(appointment =>
                requestedStart < appointment.AppointmentDate.AddMinutes(appointment.DurationMinutes ?? 30) &&
                requestedEnd > appointment.AppointmentDate,
                cancellationToken);

        return overlaps
            ? BusinessRuleResult.Failure("Doctor already has an appointment in the requested time range")
            : BusinessRuleResult.Success();
    }
}
