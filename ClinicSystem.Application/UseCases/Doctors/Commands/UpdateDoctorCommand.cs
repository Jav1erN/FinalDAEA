using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Doctors.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Doctors.Commands;

public record UpdateDoctorCommand(
    Guid DoctorId,
    Guid UserId,
    Guid SpecialtyId,
    string LicenseNumber,
    int? YearsExperience,
    decimal? ConsultationFee,
    string? Office,
    bool? IsActive,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<DoctorDto>>;

public class UpdateDoctorCommandHandler
    : IRequestHandler<UpdateDoctorCommand, Result<DoctorDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDoctorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DoctorDto>> Handle(
        UpdateDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Doctors;
        var entity = await repository.GetByIdAsync(request.DoctorId, cancellationToken);

        if (entity is null)
            return Result<DoctorDto>.Failure("Doctor not found");

        entity.UserId = request.UserId;
        entity.SpecialtyId = request.SpecialtyId;
        entity.LicenseNumber = request.LicenseNumber;
        entity.YearsExperience = request.YearsExperience;
        entity.ConsultationFee = request.ConsultationFee;
        entity.Office = request.Office;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DoctorDto>.Success(entity.ToDto());
    }
}

