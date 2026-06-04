using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Notifications.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Notifications.Commands;

public record CreateNotificationCommand(
    Guid UserId,
    Guid TypeId,
    string Channel,
    string? Status,
    string? EntityType,
    Guid? EntityId,
    string? Subject,
    string? Body,
    DateTime? ScheduledAt,
    DateTime? SentAt,
    DateTime? ReadAt
) : IRequest<Result<NotificationDto>>;

public class CreateNotificationCommandHandler
    : IRequestHandler<CreateNotificationCommand, Result<NotificationDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateNotificationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<NotificationDto>> Handle(
        CreateNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Notification
        {
            NotificationId = Guid.NewGuid(),
            UserId = request.UserId,
            TypeId = request.TypeId,
            Channel = request.Channel,
            Status = request.Status,
            EntityType = request.EntityType,
            EntityId = request.EntityId,
            Subject = request.Subject,
            Body = request.Body,
            ScheduledAt = request.ScheduledAt,
            SentAt = request.SentAt,
            ReadAt = request.ReadAt
        };

        await _unitOfWork.Repository<Notification>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<NotificationDto>.Success(entity.ToDto());
    }
}
