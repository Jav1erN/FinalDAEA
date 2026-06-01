using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Doctors.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Doctors.Commands;

public record CreateDoctorCommand(
    Guid UserId,
    Guid SpecialtyId,
    string LicenseNumber,
    int? YearsExperience,
    decimal? ConsultationFee,
    string? Office,
    bool? IsActive,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<DoctorDto>>;

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

        await _unitOfWork.Repository<Doctor>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DoctorDto>.Success(entity.ToDto());
    }
}
