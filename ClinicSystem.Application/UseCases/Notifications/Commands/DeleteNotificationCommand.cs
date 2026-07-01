using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Notifications.Commands;

public class DeleteNotificationCommand : IRequest<Result<bool>>
{
    public Guid NotificationId { get; set; } = Guid.Empty;
}

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
        var repository = _unitOfWork.Notifications;
        var entity = await repository.GetByIdAsync(request.NotificationId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Notification not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

