using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Roles.Commands;

public class UpdateRoleCommand : IRequest<Result<RoleDto>>
{
    public Guid RoleId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

public class UpdateRoleCommandHandler
    : IRequestHandler<UpdateRoleCommand, Result<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleDto>> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Roles;
        var entity = await repository.GetByIdAsync(request.RoleId, cancellationToken);

        if (entity is null)
            return Result<RoleDto>.Failure("Role not found");

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.UpdatedAt = request.UpdatedAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RoleDto>.Success(entity.ToDto());
    }
}

