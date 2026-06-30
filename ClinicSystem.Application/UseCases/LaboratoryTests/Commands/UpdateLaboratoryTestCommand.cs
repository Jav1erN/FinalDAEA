using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.LaboratoryTests.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Commands;

public record UpdateLaboratoryTestCommand(
    Guid LaboratoryTestId,
    Guid PatientId,
    Guid DoctorId,
    Guid? MedicalRecordId,
    string TestName,
    string Status,
    DateTime? RequestedDate,
    DateTime? SampleTakenDate,
    DateTime? CompletedDate,
    string? Observations,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<LaboratoryTestDto>>;

public class UpdateLaboratoryTestCommandHandler
    : IRequestHandler<UpdateLaboratoryTestCommand, Result<LaboratoryTestDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLaboratoryTestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LaboratoryTestDto>> Handle(
        UpdateLaboratoryTestCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.LaboratoryTests;
        var entity = await repository.GetByIdAsync(request.LaboratoryTestId, cancellationToken);

        if (entity is null)
            return Result<LaboratoryTestDto>.Failure("LaboratoryTest not found");

        entity.PatientId = request.PatientId;
        entity.DoctorId = request.DoctorId;
        entity.MedicalRecordId = request.MedicalRecordId;
        entity.TestName = request.TestName;
        entity.Status = request.Status;
        entity.RequestedDate = request.RequestedDate;
        entity.SampleTakenDate = request.SampleTakenDate;
        entity.CompletedDate = request.CompletedDate;
        entity.Observations = request.Observations;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<LaboratoryTestDto>.Success(entity.ToDto());
    }
}

