using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Users.Commands;

public record DeleteUserCommand(Guid UserId) : IRequest<Result<bool>>;

public class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<User>();
        var entity = await repository.GetByIdAsync(request.UserId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("User not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
