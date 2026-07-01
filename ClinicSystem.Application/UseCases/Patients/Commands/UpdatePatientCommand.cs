using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Patients.Commands;

public class UpdatePatientCommand : IRequest<Result<PatientDto>>
{
    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid? UserId { get; set; }

    public string DocumentNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? BloodType { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactPhone { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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
        var repository = _unitOfWork.Patients;
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

