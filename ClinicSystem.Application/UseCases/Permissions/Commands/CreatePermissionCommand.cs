using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Permissions.Commands;

public class CreatePermissionCommand : IRequest<Result<PermissionDto>>
{
    public string Resource { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? Description { get; set; }
}

public class CreatePermissionCommandHandler
    : IRequestHandler<CreatePermissionCommand, Result<PermissionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePermissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PermissionDto>> Handle(
        CreatePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Permission
        {
            PermissionId = Guid.NewGuid(),
            Resource = request.Resource,
            Action = request.Action,
            Description = request.Description
        };

        await _unitOfWork.Permissions.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PermissionDto>.Success(entity.ToDto());
    }
}

