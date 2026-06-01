using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Appointments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Appointments.Commands;

public record UpdateAppointmentCommand(
    Guid AppointmentId,
    Guid PatientId,
    Guid DoctorId,
    Guid StatusId,
    DateTime AppointmentDate,
    int? DurationMinutes,
    string? Reason,
    string? Notes,
    string? CancellationReason,
    Guid? RescheduledFrom,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<AppointmentDto>>;

public class UpdateAppointmentCommandHandler
    : IRequestHandler<UpdateAppointmentCommand, Result<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AppointmentDto>> Handle(
        UpdateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Appointment>();
        var entity = await repository.GetByIdAsync(request.AppointmentId, cancellationToken);

        if (entity is null)
            return Result<AppointmentDto>.Failure("Appointment not found");

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
