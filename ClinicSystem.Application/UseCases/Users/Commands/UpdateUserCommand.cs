using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Users.Commands;

public class UpdateUserCommand : IRequest<Result<UserDto>>
{
    public Guid UserId { get; set; } = Guid.Empty;

    public Guid RoleId { get; set; } = Guid.Empty;

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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
        var repository = _unitOfWork.Users;
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

