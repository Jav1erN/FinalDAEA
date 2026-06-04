using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.RefreshTokens.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.RefreshTokens.Queries;

public record GetRefreshTokenByIdQuery(Guid RefreshTokenId) : IRequest<Result<RefreshTokenDto>>;

public class GetRefreshTokenByIdQueryHandler
    : IRequestHandler<GetRefreshTokenByIdQuery, Result<RefreshTokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRefreshTokenByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RefreshTokenDto>> Handle(
        GetRefreshTokenByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<RefreshToken>()
            .GetByIdAsync(request.RefreshTokenId, cancellationToken);

        if (entity is null)
            return Result<RefreshTokenDto>.Failure("RefreshToken not found");

        return Result<RefreshTokenDto>.Success(entity.ToDto());
    }
}
