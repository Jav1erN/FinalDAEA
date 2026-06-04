using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Users.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Users.Queries;

public record GetUsersQuery : IRequest<Result<IEnumerable<UserDto>>>;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<UserDto>>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<User>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<UserDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
