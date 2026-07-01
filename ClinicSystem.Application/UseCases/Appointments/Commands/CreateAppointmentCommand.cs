using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Domain.Ports.Services;
using MediatR;

namespace ClinicSystem.Application.UseCases.Appointments.Commands;

public class CreateAppointmentCommand : IRequest<Result<AppointmentDto>>
{
    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid StatusId { get; set; } = Guid.Empty;

    public DateTime AppointmentDate { get; set; } = DateTime.UtcNow;

    public int? DurationMinutes { get; set; }

    public string? Reason { get; set; }

    public string? Notes { get; set; }

    public string? CancellationReason { get; set; }

    public Guid? RescheduledFrom { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class CreateAppointmentCommandHandler
    : IRequestHandler<CreateAppointmentCommand, Result<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppointmentSchedulingService _appointmentSchedulingService;

    public CreateAppointmentCommandHandler(
        IUnitOfWork unitOfWork,
        IAppointmentSchedulingService appointmentSchedulingService)
    {
        _unitOfWork = unitOfWork;
        _appointmentSchedulingService = appointmentSchedulingService;
    }

    public async Task<Result<AppointmentDto>> Handle(
        CreateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var schedulingResult = await _appointmentSchedulingService.EnsureCanScheduleAsync(
            request.DoctorId,
            request.AppointmentDate,
            request.DurationMinutes,
            cancellationToken: cancellationToken);

        if (!schedulingResult.IsValid)
            return Result<AppointmentDto>.Failure(schedulingResult.Error!);

        var entity = new Appointment
        {
            AppointmentId = Guid.NewGuid(),
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            StatusId = request.StatusId,
            AppointmentDate = request.AppointmentDate,
            DurationMinutes = request.DurationMinutes,
            Reason = request.Reason,
            Notes = request.Notes,
            CancellationReason = request.CancellationReason,
            RescheduledFrom = request.RescheduledFrom,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Appointments.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AppointmentDto>.Success(entity.ToDto());
    }
}

