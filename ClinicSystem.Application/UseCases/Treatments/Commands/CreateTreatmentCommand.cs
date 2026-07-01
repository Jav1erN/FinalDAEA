using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Treatments.Commands;

public class CreateTreatmentCommand : IRequest<Result<TreatmentDto>>
{
    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public string Description { get; set; } = string.Empty;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }
}

public class CreateTreatmentCommandHandler
    : IRequestHandler<CreateTreatmentCommand, Result<TreatmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTreatmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TreatmentDto>> Handle(
        CreateTreatmentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Treatment
        {
            TreatmentId = Guid.NewGuid(),
            MedicalRecordId = request.MedicalRecordId,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            Notes = request.Notes
        };

        await _unitOfWork.Treatments.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TreatmentDto>.Success(entity.ToDto());
    }
}

