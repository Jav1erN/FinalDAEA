using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Users.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Users.Commands;

public record CreateUserCommand(
    Guid RoleId,
    string Username,
    string Email,
    string PasswordHash,
    string FirstName,
    string LastName,
    string? Phone,
    bool? IsActive,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<UserDto>>;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new User
        {
            UserId = Guid.NewGuid(),
            RoleId = request.RoleId,
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            IsActive = request.IsActive,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Repository<User>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UserDto>.Success(entity.ToDto());
    }
}
