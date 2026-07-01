using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Treatments.Commands;

public class UpdateTreatmentCommand : IRequest<Result<TreatmentDto>>
{
    public Guid TreatmentId { get; set; } = Guid.Empty;

    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public string Description { get; set; } = string.Empty;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }
}

public class UpdateTreatmentCommandHandler
    : IRequestHandler<UpdateTreatmentCommand, Result<TreatmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTreatmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TreatmentDto>> Handle(
        UpdateTreatmentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Treatments;
        var entity = await repository.GetByIdAsync(request.TreatmentId, cancellationToken);

        if (entity is null)
            return Result<TreatmentDto>.Failure("Treatment not found");

        entity.MedicalRecordId = request.MedicalRecordId;
        entity.Description = request.Description;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Status = request.Status;
        entity.Notes = request.Notes;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TreatmentDto>.Success(entity.ToDto());
    }
}

