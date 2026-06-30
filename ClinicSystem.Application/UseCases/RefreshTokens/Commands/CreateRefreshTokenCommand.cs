using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.RefreshTokens.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.RefreshTokens.Commands;

public record CreateRefreshTokenCommand(
    Guid UserId,
    string TokenHash,
    DateTime ExpiresAt,
    DateTime? RevokedAt
) : IRequest<Result<RefreshTokenDto>>;

public class CreateRefreshTokenCommandHandler
    : IRequestHandler<CreateRefreshTokenCommand, Result<RefreshTokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RefreshTokenDto>> Handle(
        CreateRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new RefreshToken
        {
            RefreshTokenId = Guid.NewGuid(),
            UserId = request.UserId,
            TokenHash = request.TokenHash,
            ExpiresAt = request.ExpiresAt,
            RevokedAt = request.RevokedAt
        };

        await _unitOfWork.RefreshTokens.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RefreshTokenDto>.Success(entity.ToDto());
    }
}

