using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AppointmentStatuses.Queries;

public record GetAppointmentStatusesQuery : IRequest<Result<IEnumerable<AppointmentStatusDto>>>;

public class GetAppointmentStatusesQueryHandler
    : IRequestHandler<GetAppointmentStatusesQuery, Result<IEnumerable<AppointmentStatusDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentStatusesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<AppointmentStatusDto>>> Handle(
        GetAppointmentStatusesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.AppointmentStatuses
            .ListAsync(cancellationToken);

        return Result<IEnumerable<AppointmentStatusDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

