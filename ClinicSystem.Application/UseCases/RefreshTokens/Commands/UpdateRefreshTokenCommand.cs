using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.RefreshTokens.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.RefreshTokens.Commands;

public record UpdateRefreshTokenCommand(
    Guid RefreshTokenId,
    Guid UserId,
    string TokenHash,
    DateTime ExpiresAt,
    DateTime? RevokedAt
) : IRequest<Result<RefreshTokenDto>>;

public class UpdateRefreshTokenCommandHandler
    : IRequestHandler<UpdateRefreshTokenCommand, Result<RefreshTokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RefreshTokenDto>> Handle(
        UpdateRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<RefreshToken>();
        var entity = await repository.GetByIdAsync(request.RefreshTokenId, cancellationToken);

        if (entity is null)
            return Result<RefreshTokenDto>.Failure("RefreshToken not found");

        entity.UserId = request.UserId;
        entity.TokenHash = request.TokenHash;
        entity.ExpiresAt = request.ExpiresAt;
        entity.RevokedAt = request.RevokedAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RefreshTokenDto>.Success(entity.ToDto());
    }
}
