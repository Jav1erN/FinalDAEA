using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.NotificationTypes.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Commands;

public record CreateNotificationTypeCommand(
    string Code,
    string Name,
    string? TemplateSubject,
    string? TemplateBody,
    bool? IsActive
) : IRequest<Result<NotificationTypeDto>>;

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

        await _unitOfWork.Repository<NotificationType>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<NotificationTypeDto>.Success(entity.ToDto());
    }
}
