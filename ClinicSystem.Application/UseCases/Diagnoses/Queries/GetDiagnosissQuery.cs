using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Diagnoses.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Diagnoses.Queries;

public record GetDiagnosesQuery : IRequest<Result<IEnumerable<DiagnosisDto>>>;

public class GetDiagnosesQueryHandler
    : IRequestHandler<GetDiagnosesQuery, Result<IEnumerable<DiagnosisDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDiagnosesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<DiagnosisDto>>> Handle(
        GetDiagnosesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Diagnosis>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<DiagnosisDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
