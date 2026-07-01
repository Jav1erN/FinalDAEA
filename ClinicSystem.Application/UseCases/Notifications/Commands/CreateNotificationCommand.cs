using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Notifications.Commands;

public class CreateNotificationCommand : IRequest<Result<NotificationDto>>
{
    public Guid UserId { get; set; } = Guid.Empty;

    public Guid TypeId { get; set; } = Guid.Empty;

    public string Channel { get; set; } = string.Empty;

    public string? Status { get; set; }

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? ReadAt { get; set; }
}

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

        await _unitOfWork.Notifications.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<NotificationDto>.Success(entity.ToDto());
    }
}

