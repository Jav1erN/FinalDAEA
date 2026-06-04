using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Notifications.Commands;

public record DeleteNotificationCommand(Guid NotificationId) : IRequest<Result<bool>>;

public class DeleteNotificationCommandHandler
    : IRequestHandler<DeleteNotificationCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNotificationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Notification>();
        var entity = await repository.GetByIdAsync(request.NotificationId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Notification not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
