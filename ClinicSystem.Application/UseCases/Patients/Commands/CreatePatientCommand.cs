using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Patients.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Patients.Commands;

public record CreatePatientCommand(
    Guid? UserId,
    string DocumentNumber,
    string FirstName,
    string LastName,
    DateOnly? BirthDate,
    string? Gender,
    string? BloodType,
    string? Phone,
    string? Email,
    string? Address,
    string? EmergencyContactName,
    string? EmergencyContactPhone,
    bool? IsActive,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<PatientDto>>;

public class CreatePatientCommandHandler
    : IRequestHandler<CreatePatientCommand, Result<PatientDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PatientDto>> Handle(
        CreatePatientCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Patient
        {
            PatientId = Guid.NewGuid(),
            UserId = request.UserId,
            DocumentNumber = request.DocumentNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            BloodType = request.BloodType,
            Phone = request.Phone,
            Email = request.Email,
            Address = request.Address,
            EmergencyContactName = request.EmergencyContactName,
            EmergencyContactPhone = request.EmergencyContactPhone,
            IsActive = request.IsActive,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Repository<Patient>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PatientDto>.Success(entity.ToDto());
    }
}
