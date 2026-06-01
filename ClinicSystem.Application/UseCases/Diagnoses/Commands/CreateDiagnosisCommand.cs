using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Diagnoses.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Diagnoses.Commands;

public record CreateDiagnosisCommand(
    Guid MedicalRecordId,
    string Cie10Code,
    string? Description,
    bool? IsPrimary,
    DateTime? NotedAt
) : IRequest<Result<DiagnosisDto>>;

public class CreateDiagnosisCommandHandler
    : IRequestHandler<CreateDiagnosisCommand, Result<DiagnosisDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiagnosisCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DiagnosisDto>> Handle(
        CreateDiagnosisCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Diagnosis
        {
            DiagnosisId = Guid.NewGuid(),
            MedicalRecordId = request.MedicalRecordId,
            Cie10Code = request.Cie10Code,
            Description = request.Description,
            IsPrimary = request.IsPrimary,
            NotedAt = request.NotedAt
        };

        await _unitOfWork.Repository<Diagnosis>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DiagnosisDto>.Success(entity.ToDto());
    }
}
