using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Diagnoses.Commands;

public class UpdateDiagnosisCommand : IRequest<Result<DiagnosisDto>>
{
    public Guid DiagnosisId { get; set; } = Guid.Empty;

    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public string Cie10Code { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool? IsPrimary { get; set; }

    public DateTime? NotedAt { get; set; }
}

public class UpdateDiagnosisCommandHandler
    : IRequestHandler<UpdateDiagnosisCommand, Result<DiagnosisDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDiagnosisCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DiagnosisDto>> Handle(
        UpdateDiagnosisCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Diagnoses;
        var entity = await repository.GetByIdAsync(request.DiagnosisId, cancellationToken);

        if (entity is null)
            return Result<DiagnosisDto>.Failure("Diagnosis not found");

        entity.MedicalRecordId = request.MedicalRecordId;
        entity.Cie10Code = request.Cie10Code;
        entity.Description = request.Description;
        entity.IsPrimary = request.IsPrimary;
        entity.NotedAt = request.NotedAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DiagnosisDto>.Success(entity.ToDto());
    }
}

