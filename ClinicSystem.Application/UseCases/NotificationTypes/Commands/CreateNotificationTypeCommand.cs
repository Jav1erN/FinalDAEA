using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Commands;

public class CreateNotificationTypeCommand : IRequest<Result<NotificationTypeDto>>
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? TemplateSubject { get; set; }

    public string? TemplateBody { get; set; }

    public bool? IsActive { get; set; }
}

public class CreateNotificationTypeCommandHandler
    : IRequestHandler<CreateNotificationTypeCommand, Result<NotificationTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateNotificationTypeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<NotificationTypeDto>> Handle(
        CreateNotificationTypeCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new NotificationType
        {
            TypeId = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            TemplateSubject = request.TemplateSubject,
            TemplateBody = request.TemplateBody,
            IsActive = request.IsActive
        };

        await _unitOfWork.NotificationTypes.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<NotificationTypeDto>.Success(entity.ToDto());
    }
}

