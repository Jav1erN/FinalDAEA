using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Permissions.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Permissions.Commands;

public record CreatePermissionCommand(
    string Resource,
    string Action,
    string? Description
) : IRequest<Result<PermissionDto>>;

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

        await _unitOfWork.Repository<Permission>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PermissionDto>.Success(entity.ToDto());
    }
}
