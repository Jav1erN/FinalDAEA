using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.AppointmentStatuses.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.AppointmentStatuses.Queries;

public record GetAppointmentStatusByIdQuery(Guid StatusId) : IRequest<Result<AppointmentStatusDto>>;

public class GetAppointmentStatusByIdQueryHandler
    : IRequestHandler<GetAppointmentStatusByIdQuery, Result<AppointmentStatusDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentStatusByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AppointmentStatusDto>> Handle(
        GetAppointmentStatusByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<AppointmentStatus>()
            .GetByIdAsync(request.StatusId, cancellationToken);

        if (entity is null)
            return Result<AppointmentStatusDto>.Failure("AppointmentStatus not found");

        return Result<AppointmentStatusDto>.Success(entity.ToDto());
    }
}
