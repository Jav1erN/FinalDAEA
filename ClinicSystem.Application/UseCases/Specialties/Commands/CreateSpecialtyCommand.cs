using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Specialties.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Specialties.Commands;

public record CreateSpecialtyCommand(
    Guid DepartmentId,
    string Name,
    string? Description,
    bool? IsActive
) : IRequest<Result<SpecialtyDto>>;

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

        await _unitOfWork.Repository<Specialty>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<SpecialtyDto>.Success(entity.ToDto());
    }
}
