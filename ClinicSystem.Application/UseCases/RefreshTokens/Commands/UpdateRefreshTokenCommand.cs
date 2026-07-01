using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.RefreshTokens.Commands;

public class UpdateRefreshTokenCommand : IRequest<Result<RefreshTokenDto>>
{
    public Guid RefreshTokenId { get; set; } = Guid.Empty;

    public Guid UserId { get; set; } = Guid.Empty;

    public string TokenHash { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; }
}

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
        var repository = _unitOfWork.RefreshTokens;
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

