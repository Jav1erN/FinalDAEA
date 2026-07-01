using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Commands;

public class UpdateNotificationTypeCommand : IRequest<Result<NotificationTypeDto>>
{
    public Guid TypeId { get; set; } = Guid.Empty;

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? TemplateSubject { get; set; }

    public string? TemplateBody { get; set; }

    public bool? IsActive { get; set; }
}

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
        var repository = _unitOfWork.NotificationTypes;
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

