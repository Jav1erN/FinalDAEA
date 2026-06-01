using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Patients.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Patients.Commands;

public record UpdatePatientCommand(
    Guid PatientId,
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
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<PatientDto>>;

public class UpdatePatientCommandHandler
    : IRequestHandler<UpdatePatientCommand, Result<PatientDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatientCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PatientDto>> Handle(
        UpdatePatientCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Patient>();
        var entity = await repository.GetByIdAsync(request.PatientId, cancellationToken);

        if (entity is null)
            return Result<PatientDto>.Failure("Patient not found");

        entity.UserId = request.UserId;
        entity.DocumentNumber = request.DocumentNumber;
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.BirthDate = request.BirthDate;
        entity.Gender = request.Gender;
        entity.BloodType = request.BloodType;
        entity.Phone = request.Phone;
        entity.Email = request.Email;
        entity.Address = request.Address;
        entity.EmergencyContactName = request.EmergencyContactName;
        entity.EmergencyContactPhone = request.EmergencyContactPhone;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PatientDto>.Success(entity.ToDto());
    }
}
