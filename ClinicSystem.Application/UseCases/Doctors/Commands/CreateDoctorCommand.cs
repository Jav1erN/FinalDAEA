using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Doctors.Commands;

public class CreateDoctorCommand : IRequest<Result<DoctorDto>>
{
    public Guid UserId { get; set; } = Guid.Empty;

    public Guid SpecialtyId { get; set; } = Guid.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public int? YearsExperience { get; set; }

    public decimal? ConsultationFee { get; set; }

    public string? Office { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class CreateDoctorCommandHandler
    : IRequestHandler<CreateDoctorCommand, Result<DoctorDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDoctorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DoctorDto>> Handle(
        CreateDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Doctor
        {
            DoctorId = Guid.NewGuid(),
            UserId = request.UserId,
            SpecialtyId = request.SpecialtyId,
            LicenseNumber = request.LicenseNumber,
            YearsExperience = request.YearsExperience,
            ConsultationFee = request.ConsultationFee,
            Office = request.Office,
            IsActive = request.IsActive,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Doctors.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DoctorDto>.Success(entity.ToDto());
    }
}

