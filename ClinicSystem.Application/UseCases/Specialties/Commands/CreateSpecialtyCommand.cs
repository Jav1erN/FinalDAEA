using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Specialties.Commands;

public class CreateSpecialtyCommand : IRequest<Result<SpecialtyDto>>
{
    public Guid DepartmentId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}

public class CreateSpecialtyCommandHandler
    : IRequestHandler<CreateSpecialtyCommand, Result<SpecialtyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpecialtyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SpecialtyDto>> Handle(
        CreateSpecialtyCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Specialty
        {
            SpecialtyId = Guid.NewGuid(),
            DepartmentId = request.DepartmentId,
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive
        };

        await _unitOfWork.Specialties.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<SpecialtyDto>.Success(entity.ToDto());
    }
}

