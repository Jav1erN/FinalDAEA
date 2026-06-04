using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.LaboratoryTests.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Commands;

public record CreateLaboratoryTestCommand(
    Guid PatientId,
    Guid DoctorId,
    Guid? MedicalRecordId,
    string TestName,
    string Status,
    DateTime? RequestedDate,
    DateTime? SampleTakenDate,
    DateTime? CompletedDate,
    string? Observations,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<LaboratoryTestDto>>;

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

        await _unitOfWork.Repository<LaboratoryTest>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<LaboratoryTestDto>.Success(entity.ToDto());
    }
}
