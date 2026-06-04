using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Treatments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Treatments.Queries;

public record GetTreatmentsQuery : IRequest<Result<IEnumerable<TreatmentDto>>>;

public class GetTreatmentsQueryHandler
    : IRequestHandler<GetTreatmentsQuery, Result<IEnumerable<TreatmentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTreatmentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<TreatmentDto>>> Handle(
        GetTreatmentsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Treatment>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<TreatmentDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
