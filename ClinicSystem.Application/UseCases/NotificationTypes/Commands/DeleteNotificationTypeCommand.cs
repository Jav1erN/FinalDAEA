using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Commands;

public record DeleteNotificationTypeCommand(Guid TypeId) : IRequest<Result<bool>>;

public class DeleteNotificationTypeCommandHandler
    : IRequestHandler<DeleteNotificationTypeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNotificationTypeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteNotificationTypeCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<NotificationType>();
        var entity = await repository.GetByIdAsync(request.TypeId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("NotificationType not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
