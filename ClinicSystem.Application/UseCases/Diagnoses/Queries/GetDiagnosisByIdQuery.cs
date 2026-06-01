using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Diagnoses.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Diagnoses.Queries;

public record GetDiagnosisByIdQuery(Guid DiagnosisId) : IRequest<Result<DiagnosisDto>>;

public class GetDiagnosisByIdQueryHandler
    : IRequestHandler<GetDiagnosisByIdQuery, Result<DiagnosisDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDiagnosisByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DiagnosisDto>> Handle(
        GetDiagnosisByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Diagnosis>()
            .GetByIdAsync(request.DiagnosisId, cancellationToken);

        if (entity is null)
            return Result<DiagnosisDto>.Failure("Diagnosis not found");

        return Result<DiagnosisDto>.Success(entity.ToDto());
    }
}
