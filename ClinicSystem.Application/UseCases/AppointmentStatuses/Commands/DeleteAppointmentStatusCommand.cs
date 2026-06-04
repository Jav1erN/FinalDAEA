using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.AppointmentStatuses.Commands;

public record DeleteAppointmentStatusCommand(Guid StatusId) : IRequest<Result<bool>>;

public class DeleteAppointmentStatusCommandHandler
    : IRequestHandler<DeleteAppointmentStatusCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteAppointmentStatusCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<AppointmentStatus>();
        var entity = await repository.GetByIdAsync(request.StatusId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("AppointmentStatus not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
