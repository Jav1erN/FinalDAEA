using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AppointmentStatuses.Commands;

public class UpdateAppointmentStatusCommand : IRequest<Result<AppointmentStatusDto>>
{
    public Guid StatusId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}

public class UpdateAppointmentStatusCommandHandler
    : IRequestHandler<UpdateAppointmentStatusCommand, Result<AppointmentStatusDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AppointmentStatusDto>> Handle(
        UpdateAppointmentStatusCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.AppointmentStatuses;
        var entity = await repository.GetByIdAsync(request.StatusId, cancellationToken);

        if (entity is null)
            return Result<AppointmentStatusDto>.Failure("AppointmentStatus not found");

        entity.Name = request.Name;
        entity.Description = request.Description;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AppointmentStatusDto>.Success(entity.ToDto());
    }
}

