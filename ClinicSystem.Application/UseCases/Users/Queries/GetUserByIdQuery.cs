using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Users.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Users.Queries;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDto>>;

public class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<User>()
            .GetByIdAsync(request.UserId, cancellationToken);

        if (entity is null)
            return Result<UserDto>.Failure("User not found");

        return Result<UserDto>.Success(entity.ToDto());
    }
}
