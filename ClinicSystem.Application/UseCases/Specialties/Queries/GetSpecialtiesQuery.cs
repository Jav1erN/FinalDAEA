using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Specialties.Queries;

public record GetSpecialtiesQuery : IRequest<Result<IEnumerable<SpecialtyDto>>>;

public class GetSpecialtiesQueryHandler
    : IRequestHandler<GetSpecialtiesQuery, Result<IEnumerable<SpecialtyDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSpecialtiesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<SpecialtyDto>>> Handle(
        GetSpecialtiesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Specialties
            .ListAsync(cancellationToken);

        return Result<IEnumerable<SpecialtyDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

