using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Users.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Users.Commands;

public record UpdateUserCommand(
    Guid UserId,
    Guid RoleId,
    string Username,
    string Email,
    string PasswordHash,
    string FirstName,
    string LastName,
    string? Phone,
    bool? IsActive,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<UserDto>>;

public class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, Result<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<User>();
        var entity = await repository.GetByIdAsync(request.UserId, cancellationToken);

        if (entity is null)
            return Result<UserDto>.Failure("User not found");

        entity.RoleId = request.RoleId;
        entity.Username = request.Username;
        entity.Email = request.Email;
        entity.PasswordHash = request.PasswordHash;
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Phone = request.Phone;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UserDto>.Success(entity.ToDto());
    }
}
