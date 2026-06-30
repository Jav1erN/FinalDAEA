using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.PrescriptionDetails.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Commands;

public record UpdatePrescriptionDetailCommand(
    Guid PrescriptionDetailId,
    Guid PrescriptionId,
    Guid MedicationId,
    string? Dosage,
    string? Frequency,
    int? DurationDays,
    int QuantityPrescribed,
    string? Instructions,
    bool? IsSubstitutable
) : IRequest<Result<PrescriptionDetailDto>>;

public class UpdatePrescriptionDetailCommandHandler
    : IRequestHandler<UpdatePrescriptionDetailCommand, Result<PrescriptionDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePrescriptionDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PrescriptionDetailDto>> Handle(
        UpdatePrescriptionDetailCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.PrescriptionDetails;
        var entity = await repository.GetByIdAsync(request.PrescriptionDetailId, cancellationToken);

        if (entity is null)
            return Result<PrescriptionDetailDto>.Failure("PrescriptionDetail not found");

        entity.PrescriptionId = request.PrescriptionId;
        entity.MedicationId = request.MedicationId;
        entity.Dosage = request.Dosage;
        entity.Frequency = request.Frequency;
        entity.DurationDays = request.DurationDays;
        entity.QuantityPrescribed = request.QuantityPrescribed;
        entity.Instructions = request.Instructions;
        entity.IsSubstitutable = request.IsSubstitutable;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PrescriptionDetailDto>.Success(entity.ToDto());
    }
}

