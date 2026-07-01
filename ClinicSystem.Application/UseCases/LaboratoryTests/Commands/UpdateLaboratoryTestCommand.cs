using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Commands;

public class UpdateLaboratoryTestCommand : IRequest<Result<LaboratoryTestDto>>
{
    public Guid LaboratoryTestId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid? MedicalRecordId { get; set; }

    public string TestName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime? RequestedDate { get; set; }

    public DateTime? SampleTakenDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public string? Observations { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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

