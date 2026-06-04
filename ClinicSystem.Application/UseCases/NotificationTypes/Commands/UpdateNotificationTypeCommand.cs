using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.NotificationTypes.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Commands;

public record UpdateNotificationTypeCommand(
    Guid TypeId,
    string Code,
    string Name,
    string? TemplateSubject,
    string? TemplateBody,
    bool? IsActive
) : IRequest<Result<NotificationTypeDto>>;

public class UpdateNotificationTypeCommandHandler
    : IRequestHandler<UpdateNotificationTypeCommand, Result<NotificationTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNotificationTypeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<NotificationTypeDto>> Handle(
        UpdateNotificationTypeCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<NotificationType>();
        var entity = await repository.GetByIdAsync(request.TypeId, cancellationToken);

        if (entity is null)
            return Result<NotificationTypeDto>.Failure("NotificationType not found");

        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.TemplateSubject = request.TemplateSubject;
        entity.TemplateBody = request.TemplateBody;
        entity.IsActive = request.IsActive;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<NotificationTypeDto>.Success(entity.ToDto());
    }
}
