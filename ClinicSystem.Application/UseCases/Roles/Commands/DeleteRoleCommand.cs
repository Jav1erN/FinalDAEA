using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Roles.Commands;

public record DeleteRoleCommand(Guid RoleId) : IRequest<Result<bool>>;

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
        var repository = _unitOfWork.Repository<Role>();
        var entity = await repository.GetByIdAsync(request.RoleId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Role not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
