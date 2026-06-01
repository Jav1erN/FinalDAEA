using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.RefreshTokens.Commands;

public record DeleteRefreshTokenCommand(Guid RefreshTokenId) : IRequest<Result<bool>>;

public class DeleteRefreshTokenCommandHandler
    : IRequestHandler<DeleteRefreshTokenCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<RefreshToken>();
        var entity = await repository.GetByIdAsync(request.RefreshTokenId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("RefreshToken not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
