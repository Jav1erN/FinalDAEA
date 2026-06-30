using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.VitalSigns.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.VitalSigns.Queries;

public record GetVitalSignsQuery : IRequest<Result<IEnumerable<VitalSignDto>>>;

public class GetVitalSignsQueryHandler
    : IRequestHandler<GetVitalSignsQuery, Result<IEnumerable<VitalSignDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetVitalSignsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<VitalSignDto>>> Handle(
        GetVitalSignsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.VitalSigns
            .ListAsync(cancellationToken);

        return Result<IEnumerable<VitalSignDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

