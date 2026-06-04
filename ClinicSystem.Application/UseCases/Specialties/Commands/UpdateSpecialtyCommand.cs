using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Specialties.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Specialties.Commands;

public record UpdateSpecialtyCommand(
    Guid SpecialtyId,
    Guid DepartmentId,
    string Name,
    string? Description,
    bool? IsActive,
    DateTime? UpdatedAt
) : IRequest<Result<SpecialtyDto>>;

public class UpdateSpecialtyCommandHandler
    : IRequestHandler<UpdateSpecialtyCommand, Result<SpecialtyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSpecialtyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SpecialtyDto>> Handle(
        UpdateSpecialtyCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Specialty>();
        var entity = await repository.GetByIdAsync(request.SpecialtyId, cancellationToken);

        if (entity is null)
            return Result<SpecialtyDto>.Failure("Specialty not found");

        entity.DepartmentId = request.DepartmentId;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = request.UpdatedAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<SpecialtyDto>.Success(entity.ToDto());
    }
}
