using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Specialties.Queries;

public record GetSpecialtyByIdQuery(Guid SpecialtyId) : IRequest<Result<SpecialtyDto>>;

public class GetSpecialtyByIdQueryHandler
    : IRequestHandler<GetSpecialtyByIdQuery, Result<SpecialtyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSpecialtyByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SpecialtyDto>> Handle(
        GetSpecialtyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Specialties
            .GetByIdAsync(request.SpecialtyId, cancellationToken);

        if (entity is null)
            return Result<SpecialtyDto>.Failure("Specialty not found");

        return Result<SpecialtyDto>.Success(entity.ToDto());
    }
}

