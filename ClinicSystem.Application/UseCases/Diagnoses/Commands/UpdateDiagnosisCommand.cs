using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Diagnoses.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Diagnoses.Commands;

public record UpdateDiagnosisCommand(
    Guid DiagnosisId,
    Guid MedicalRecordId,
    string Cie10Code,
    string? Description,
    bool? IsPrimary,
    DateTime? NotedAt
) : IRequest<Result<DiagnosisDto>>;

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
        var repository = _unitOfWork.Repository<Diagnosis>();
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
