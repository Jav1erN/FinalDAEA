using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.AppointmentStatuses.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AppointmentStatuses.Commands;

public record CreateAppointmentStatusCommand(
    string Name,
    string? Description
) : IRequest<Result<AppointmentStatusDto>>;

public class CreateAppointmentStatusCommandHandler
    : IRequestHandler<CreateAppointmentStatusCommand, Result<AppointmentStatusDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AppointmentStatusDto>> Handle(
        CreateAppointmentStatusCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new AppointmentStatus
        {
            StatusId = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        await _unitOfWork.Repository<AppointmentStatus>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AppointmentStatusDto>.Success(entity.ToDto());
    }
}
