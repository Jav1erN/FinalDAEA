using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Medications.Commands;

public class CreateMedicationCommand : IRequest<Result<MedicationDto>>
{
    public string Name { get; set; } = string.Empty;

    public string? GenericName { get; set; }

    public string? Presentation { get; set; }

    public string? Concentration { get; set; }

    public string? Laboratory { get; set; }

    public bool? RequiresPrescription { get; set; }

    public int? Stock { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class CreateMedicationCommandHandler
    : IRequestHandler<CreateMedicationCommand, Result<MedicationDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMedicationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MedicationDto>> Handle(
        CreateMedicationCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Medication
        {
            MedicationId = Guid.NewGuid(),
            Name = request.Name,
            GenericName = request.GenericName,
            Presentation = request.Presentation,
            Concentration = request.Concentration,
            Laboratory = request.Laboratory,
            RequiresPrescription = request.RequiresPrescription,
            Stock = request.Stock,
            UnitPrice = request.UnitPrice,
            IsActive = request.IsActive,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Medications.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MedicationDto>.Success(entity.ToDto());
    }
}

