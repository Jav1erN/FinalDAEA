using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AppointmentStatuses.Commands;

public class CreateAppointmentStatusCommand : IRequest<Result<AppointmentStatusDto>>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}

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

        await _unitOfWork.AppointmentStatuses.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AppointmentStatusDto>.Success(entity.ToDto());
    }
}

