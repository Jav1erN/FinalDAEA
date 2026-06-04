using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.LaboratoryResults.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Queries;

public record GetLaboratoryResultsQuery : IRequest<Result<IEnumerable<LaboratoryResultDto>>>;

public class GetLaboratoryResultsQueryHandler
    : IRequestHandler<GetLaboratoryResultsQuery, Result<IEnumerable<LaboratoryResultDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLaboratoryResultsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<LaboratoryResultDto>>> Handle(
        GetLaboratoryResultsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<LaboratoryResult>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<LaboratoryResultDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
