using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Treatments.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Treatments.Commands;

public record UpdateTreatmentCommand(
    Guid TreatmentId,
    Guid MedicalRecordId,
    string Description,
    DateOnly? StartDate,
    DateOnly? EndDate,
    string? Status,
    string? Notes
) : IRequest<Result<TreatmentDto>>;

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
        var repository = _unitOfWork.Repository<Treatment>();
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
