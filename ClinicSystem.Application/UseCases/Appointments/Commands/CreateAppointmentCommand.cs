using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Appointments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Appointments.Commands;

public record CreateAppointmentCommand(
    Guid PatientId,
    Guid DoctorId,
    Guid StatusId,
    DateTime AppointmentDate,
    int? DurationMinutes,
    string? Reason,
    string? Notes,
    string? CancellationReason,
    Guid? RescheduledFrom,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<AppointmentDto>>;

public class CreateAppointmentCommandHandler
    : IRequestHandler<CreateAppointmentCommand, Result<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AppointmentDto>> Handle(
        CreateAppointmentCommand request,
        CancellationToken cancellationToken)
    {
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

        await _unitOfWork.Repository<Appointment>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AppointmentDto>.Success(entity.ToDto());
    }
}
