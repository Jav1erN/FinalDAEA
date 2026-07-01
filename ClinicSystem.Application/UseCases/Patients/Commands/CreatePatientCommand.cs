using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Patients.Commands;

public class CreatePatientCommand : IRequest<Result<PatientDto>>
{
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

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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

        await _unitOfWork.Patients.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PatientDto>.Success(entity.ToDto());
    }
}

