using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Appointments.Queries;

public record GetAppointmentByIdQuery(Guid AppointmentId) : IRequest<Result<AppointmentDto>>;

public class GetAppointmentByIdQueryHandler
    : IRequestHandler<GetAppointmentByIdQuery, Result<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AppointmentDto>> Handle(
        GetAppointmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Appointments
            .GetByIdAsync(request.AppointmentId, cancellationToken);

        if (entity is null)
            return Result<AppointmentDto>.Failure("Appointment not found");

        return Result<AppointmentDto>.Success(entity.ToDto());
    }
}

