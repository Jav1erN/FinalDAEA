using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Specialties.Commands;

public class UpdateSpecialtyCommand : IRequest<Result<SpecialtyDto>>
{
    public Guid SpecialtyId { get; set; } = Guid.Empty;

    public Guid DepartmentId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

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
        var repository = _unitOfWork.Specialties;
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

