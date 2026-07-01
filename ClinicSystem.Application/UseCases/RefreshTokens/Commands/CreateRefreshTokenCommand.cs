using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.RefreshTokens.Commands;

public class CreateRefreshTokenCommand : IRequest<Result<RefreshTokenDto>>
{
    public Guid UserId { get; set; } = Guid.Empty;

    public string TokenHash { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; }
}

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

