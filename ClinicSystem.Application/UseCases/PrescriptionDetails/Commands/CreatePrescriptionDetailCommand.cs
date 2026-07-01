using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Commands;

public class CreatePrescriptionDetailCommand : IRequest<Result<PrescriptionDetailDto>>
{
    public Guid PrescriptionId { get; set; } = Guid.Empty;

    public Guid MedicationId { get; set; } = Guid.Empty;

    public string? Dosage { get; set; }

    public string? Frequency { get; set; }

    public int? DurationDays { get; set; }

    public int QuantityPrescribed { get; set; }

    public string? Instructions { get; set; }

    public bool? IsSubstitutable { get; set; }
}

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

        await _unitOfWork.PrescriptionDetails.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PrescriptionDetailDto>.Success(entity.ToDto());
    }
}

