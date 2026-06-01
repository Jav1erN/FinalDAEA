using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.PrescriptionDetails.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Commands;

public record CreatePrescriptionDetailCommand(
    Guid PrescriptionId,
    Guid MedicationId,
    string? Dosage,
    string? Frequency,
    int? DurationDays,
    int QuantityPrescribed,
    string? Instructions,
    bool? IsSubstitutable
) : IRequest<Result<PrescriptionDetailDto>>;

public class CreatePrescriptionDetailCommandHandler
    : IRequestHandler<CreatePrescriptionDetailCommand, Result<PrescriptionDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePrescriptionDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PrescriptionDetailDto>> Handle(
        CreatePrescriptionDetailCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new PrescriptionDetail
        {
            PrescriptionDetailId = Guid.NewGuid(),
            PrescriptionId = request.PrescriptionId,
            MedicationId = request.MedicationId,
            Dosage = request.Dosage,
            Frequency = request.Frequency,
            DurationDays = request.DurationDays,
            QuantityPrescribed = request.QuantityPrescribed,
            Instructions = request.Instructions,
            IsSubstitutable = request.IsSubstitutable
        };

        await _unitOfWork.Repository<PrescriptionDetail>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PrescriptionDetailDto>.Success(entity.ToDto());
    }
}
