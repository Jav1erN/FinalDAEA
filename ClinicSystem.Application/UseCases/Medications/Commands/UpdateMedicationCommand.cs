using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Medications.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Medications.Commands;

public record UpdateMedicationCommand(
    Guid MedicationId,
    string Name,
    string? GenericName,
    string? Presentation,
    string? Concentration,
    string? Laboratory,
    bool? RequiresPrescription,
    int? Stock,
    decimal? UnitPrice,
    bool? IsActive,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<MedicationDto>>;

public class UpdateMedicationCommandHandler
    : IRequestHandler<UpdateMedicationCommand, Result<MedicationDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMedicationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MedicationDto>> Handle(
        UpdateMedicationCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Medications;
        var entity = await repository.GetByIdAsync(request.MedicationId, cancellationToken);

        if (entity is null)
            return Result<MedicationDto>.Failure("Medication not found");

        entity.Name = request.Name;
        entity.GenericName = request.GenericName;
        entity.Presentation = request.Presentation;
        entity.Concentration = request.Concentration;
        entity.Laboratory = request.Laboratory;
        entity.RequiresPrescription = request.RequiresPrescription;
        entity.Stock = request.Stock;
        entity.UnitPrice = request.UnitPrice;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MedicationDto>.Success(entity.ToDto());
    }
}

