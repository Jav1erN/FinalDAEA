using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Medications.Commands;

public class UpdateMedicationCommand : IRequest<Result<MedicationDto>>
{
    public Guid MedicationId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? GenericName { get; set; }

    public string? Presentation { get; set; }

    public string? Concentration { get; set; }

    public string? Laboratory { get; set; }

    public bool? RequiresPrescription { get; set; }

    public int? Stock { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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

