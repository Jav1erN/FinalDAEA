using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.RefreshTokens.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.RefreshTokens.Queries;

public record GetRefreshTokensQuery : IRequest<Result<IEnumerable<RefreshTokenDto>>>;

public class GetRefreshTokensQueryHandler
    : IRequestHandler<GetRefreshTokensQuery, Result<IEnumerable<RefreshTokenDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRefreshTokensQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<RefreshTokenDto>>> Handle(
        GetRefreshTokensQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<RefreshToken>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<RefreshTokenDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
