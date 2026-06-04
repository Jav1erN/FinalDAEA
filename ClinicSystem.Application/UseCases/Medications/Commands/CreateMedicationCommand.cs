using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Medications.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Medications.Commands;

public record CreateMedicationCommand(
    string Name,
    string? GenericName,
    string? Presentation,
    string? Concentration,
    string? Laboratory,
    bool? RequiresPrescription,
    int? Stock,
    decimal? UnitPrice,
    bool? IsActive,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<MedicationDto>>;

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

        await _unitOfWork.Repository<Medication>()
            .AddAsync(entity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MedicationDto>.Success(entity.ToDto());
    }
}
