using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Roles.Commands;

public class DeleteRoleCommand : IRequest<Result<bool>>
{
    public Guid RoleId { get; set; } = Guid.Empty;
}

public class DeleteRoleCommandHandler
    : IRequestHandler<DeleteRoleCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Roles;
        var entity = await repository.GetByIdAsync(request.RoleId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Role not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

