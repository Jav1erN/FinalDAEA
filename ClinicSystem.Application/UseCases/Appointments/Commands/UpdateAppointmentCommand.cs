using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Domain.Ports.Services;
using MediatR;

namespace ClinicSystem.Application.UseCases.Appointments.Commands;

public class UpdateAppointmentCommand : IRequest<Result<AppointmentDto>>
{
    public Guid AppointmentId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid StatusId { get; set; } = Guid.Empty;

    public DateTime AppointmentDate { get; set; } = DateTime.UtcNow;

    public int? DurationMinutes { get; set; }

    public string? Reason { get; set; }

    public string? Notes { get; set; }

    public string? CancellationReason { get; set; }

    public Guid? RescheduledFrom { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class UpdateAppointmentCommandHandler
    : IRequestHandler<UpdateAppointmentCommand, Result<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppointmentSchedulingService _appointmentSchedulingService;

    public UpdateAppointmentCommandHandler(
        IUnitOfWork unitOfWork,
        IAppointmentSchedulingService appointmentSchedulingService)
    {
        _unitOfWork = unitOfWork;
        _appointmentSchedulingService = appointmentSchedulingService;
    }

    public async Task<Result<AppointmentDto>> Handle(
        UpdateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Appointments;
        var entity = await repository.GetByIdAsync(request.AppointmentId, cancellationToken);

        if (entity is null)
            return Result<AppointmentDto>.Failure("Appointment not found");

        var schedulingResult = await _appointmentSchedulingService.EnsureCanScheduleAsync(
            request.DoctorId,
            request.AppointmentDate,
            request.DurationMinutes,
            request.AppointmentId,
            cancellationToken);

        if (!schedulingResult.IsValid)
            return Result<AppointmentDto>.Failure(schedulingResult.Error!);

        entity.PatientId = request.PatientId;
        entity.DoctorId = request.DoctorId;
        entity.StatusId = request.StatusId;
        entity.AppointmentDate = request.AppointmentDate;
        entity.DurationMinutes = request.DurationMinutes;
        entity.Reason = request.Reason;
        entity.Notes = request.Notes;
        entity.CancellationReason = request.CancellationReason;
        entity.RescheduledFrom = request.RescheduledFrom;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AppointmentDto>.Success(entity.ToDto());
    }
}

