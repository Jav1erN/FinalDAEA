using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Permissions.Commands;

public class UpdatePermissionCommand : IRequest<Result<PermissionDto>>
{
    public Guid PermissionId { get; set; } = Guid.Empty;

    public string Resource { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? Description { get; set; }
}

public class UpdatePermissionCommandHandler
    : IRequestHandler<UpdatePermissionCommand, Result<PermissionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PermissionDto>> Handle(
        UpdatePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Permissions;
        var entity = await repository.GetByIdAsync(request.PermissionId, cancellationToken);

        if (entity is null)
            return Result<PermissionDto>.Failure("Permission not found");

        entity.Resource = request.Resource;
        entity.Action = request.Action;
        entity.Description = request.Description;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PermissionDto>.Success(entity.ToDto());
    }
}

