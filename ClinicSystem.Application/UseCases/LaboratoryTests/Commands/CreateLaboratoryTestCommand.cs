using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Commands;

public class CreateLaboratoryTestCommand : IRequest<Result<LaboratoryTestDto>>
{
    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid? MedicalRecordId { get; set; }

    public string TestName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime? RequestedDate { get; set; }

    public DateTime? SampleTakenDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public string? Observations { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class CreateLaboratoryTestCommandHandler
    : IRequestHandler<CreateLaboratoryTestCommand, Result<LaboratoryTestDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLaboratoryTestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LaboratoryTestDto>> Handle(
        CreateLaboratoryTestCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new LaboratoryTest
        {
            LaboratoryTestId = Guid.NewGuid(),
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            MedicalRecordId = request.MedicalRecordId,
            TestName = request.TestName,
            Status = request.Status,
            RequestedDate = request.RequestedDate,
            SampleTakenDate = request.SampleTakenDate,
            CompletedDate = request.CompletedDate,
            Observations = request.Observations,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.LaboratoryTests.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<LaboratoryTestDto>.Success(entity.ToDto());
    }
}

